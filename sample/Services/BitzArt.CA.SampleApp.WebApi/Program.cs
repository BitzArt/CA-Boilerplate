using BitzArt.CA;
using BitzArt.CA.Persistence;
using BitzArt.CA.SampleApp;
using BitzArt.CA.SampleApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.AddTelemetry();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Init(x =>
{
    x.InitPersistence();
});

app.Run();
