using IotRemoteLab.API;
using IotRemoteLab.API.CLI;
using IotRemoteLab.API.CLI.Commands;
using IotRemoteLab.API.HostBuilderExtensions;
using IotRemoteLab.API.Hubs;
using IotRemoteLab.API.Services;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();


#region Mqtt Prepare


var mqttConnectionType = builder.Configuration.GetSection("Mqtt:ConnectionType").Value;

if (string.IsNullOrEmpty(mqttConnectionType)) 
{
    throw new Exception("Mqtt ConnectionType must be Certificated/NoCertificated.");
}

var mqttIp = builder.Configuration.GetSection($"Mqtt:{mqttConnectionType}:Ip").Value;
var mqttPortString = builder.Configuration.GetSection($"Mqtt:{mqttConnectionType}:Port").Value;

if (string.IsNullOrEmpty(mqttIp)) 
{
    throw new Exception("MQTT Ip must be not empty");
}

if (!short.TryParse(mqttPortString, out var mqttPort))
{
    throw new Exception("MQTT Port must be not empty/null or higher than 65565");
}

if (mqttConnectionType == "Certificated") 
{
    var caCertFilePath = builder.Configuration.GetSection("Mqtt:Certificated:CertificateFilePath:Ca").Value;
    var clientCertFilePath = builder.Configuration.GetSection("Mqtt:Certificated:CertificateFilePath:Client").Value;

    if (!(string.IsNullOrEmpty(caCertFilePath) && string.IsNullOrEmpty(clientCertFilePath)))
    {
        var ca = X509Certificate.CreateFromCertFile(caCertFilePath);
        var client = new X509Certificate2(clientCertFilePath);
        builder.Services.AddMqtt(mqttIp, mqttPort, ca, client, Topics.ToArray());
    }
    else
    {
        throw new Exception("CertificateFilePath.[Ca/Client] must be not empty with connection type Certificated");
    }
}
else 
{
    builder.Services.AddMqtt(mqttIp, mqttPort, Topics.ToArray());
}


#endregion MqttPrepare


builder.Services.AddCLI();

builder.Services.AddSingleton<StandHubBroadcast>();
builder.Services.AddSingleton<StandsService>();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHub<StandHub>("/stand-hub");

app.UseMqtt();
app.Services.GetRequiredService<StandHubBroadcast>();

app.Run();
