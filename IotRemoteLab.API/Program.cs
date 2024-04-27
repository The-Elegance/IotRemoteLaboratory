using IotRemoteLab.API.Hubs;
using IotRemoteLaboratory.Mqtt.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddSingleton(_ => new MqttParams("test.mosquitto.org", 1883, "/lab/stand/#/debug/upload"));
builder.Services.AddSingleton<MqttPublisher>();
builder.Services.AddSingleton<MqttSubscriber>();

var app = builder.Build();


app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

var subscriber = app.Services.GetService(typeof(MqttSubscriber));
//subscriber

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

app.Run();
