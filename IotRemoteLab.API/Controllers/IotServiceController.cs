using System.Collections.Concurrent;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using IotRemoteLab.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;

public class IotServiceController
{
    private readonly Dictionary<IotServiceType, IUpperIotService> _uppersIotServices;

    public IotServiceController(IUsersRepository usersRepository,
        IScheduleRepository repository, IEnumerable<IUpperIotService> upperIotServices)
    {
        _uppersIotServices = upperIotServices.ToDictionary(k => k.Type);
    }


    [HttpPost("upIotServiceByMe/{standId}")]
    public async Task<ActionResult<string>> UpIotService([FromBody] IotServiceType type, [FromRoute]Guid standId)
    {
        return await _uppersIotServices[type].UpServiceAsync();
    }
}

public enum IotServiceType
{
    Mqqt,
    NodeRed
}


public interface IUpperIotService : IAsyncDisposable
{
    IotServiceType Type { get; }

    public Task<string> UpServiceAsync();
}


public class UpperMqqtService : IUpperIotService
{
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public IotServiceType Type { get; }
    public Task<string> UpServiceAsync()
    {
        throw new NotImplementedException();
    }
}

public class UpperNodeRedService : IUpperIotService
{
    private ConcurrentBag<IContainer> _containers = new() ;

    public IotServiceType Type => IotServiceType.NodeRed;
    public  async Task<string> UpServiceAsync()
    {
        var container = new ContainerBuilder()
            .WithImage("nodered/node-red-docker:slim-v8")
            .WithPortBinding(1880, true)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilHttpRequestIsSucceeded(r => r.ForPort(1880)))
            .Build();

        await container.StartAsync()
            .ConfigureAwait(false);

        _containers.Add(container);
        
        return $"{container.Hostname}:{container.GetMappedPublicPort(1880)}";
    }
    
    public async ValueTask DisposeAsync()
    {
        foreach (var container in _containers)
        {
            await container.StopAsync();
            await container.DisposeAsync();
        }
            
    }
}
