using IotRemoteLab.API;
using IotRemoteLab.API.HostBuilderExtentions;
using IotRemoteLab.API.Hubs;
using IotRemoteLab.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
#if DEBUG
builder.Services.AddMqtt(Topics.ToArray());
#else
builder.Services.AddMqtt("some ip", 0000, Topics.ToArray());
#endif

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
