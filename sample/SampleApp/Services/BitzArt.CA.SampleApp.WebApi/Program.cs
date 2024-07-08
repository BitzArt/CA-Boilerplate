using BitzArt.CA;
using BitzArt.CA.Persistence;
using BitzArt.CA.SampleApp.Infrastructure;
using BitzArt.CA.SampleApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersistence();
builder.AddAddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapControllers();

app.Init(x =>
{
    x.InitPersistence();
});

app.Run();
