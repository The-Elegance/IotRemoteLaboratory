using IotRemoteLab.API;
using IotRemoteLab.API.HostBuilderExtensions;
using IotRemoteLab.API.Hubs;
using IotRemoteLab.API.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using IotRemoteLab.API.Controllers;
using IotRemoteLab.API.Repositories;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<RolesRepository>();
//builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddSingleton<IUpperIotService, UpperNodeRedService>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Please enter your token with this format: ''Bearer YOUR_TOKEN''",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection(ConfigPath.JwtKey).Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
        };
    });

builder.Services.ConfigureHttpJsonOptions(p => p.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DebugConnection")//,
                                                                                  //x => x.MigrationsAssembly("IotRemoteLab.Persistence")
        );
});


builder.Services.AddSignalR();


#region Mqtt Prepare


try
{

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
}
catch { }


#endregion MqttPrepare


builder.Services.AddCLI();

builder.Services.AddSingleton<StandHubBroadcast>();
builder.Services.AddSingleton<StandsService>();

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
