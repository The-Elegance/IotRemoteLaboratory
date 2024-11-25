using IotRemoteLab.Application;
using IotRemoteLab.Domain.Team;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers;


[Route("api/teams")]
public class TeamController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public TeamController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    
    [HttpPost]
    public Team CreateTeam([FromBody] TeamCreateDto dto)
    {
        var a = _applicationContext.Users
            .Where(user => dto.UserIds.Contains(user.Id))
            .ToArray();
        var team = new Team()
        {
            Id = Guid.NewGuid(),
            Name = dto.TeamName,
            Members = a
        };
        
        _applicationContext.Teams.Add(team);
        _applicationContext.SaveChanges();
        return team;
    }
    
    
    [HttpGet("{teamId:guid}")]
    public Team? Team([FromRoute] Guid teamId)
    {
        return _applicationContext.Teams.SingleOrDefault(team => team.Id == teamId);
    }
}