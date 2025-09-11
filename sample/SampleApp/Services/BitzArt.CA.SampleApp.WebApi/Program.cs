using BitzArt.ApiExceptions.AspNetCore;
using BitzArt.CA.Persistence;
using BitzArt.CA.SampleApp.Infrastructure;
using BitzArt.CA.SampleApp.Persistence;

namespace BitzArt.CA.SampleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddPersistence();
        builder.AddAddInfrastructure();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiExceptionHandler(handler =>
        {
            handler.DisplayInnerExceptions = true;
        });

        var app = builder.Build();

        app.UseApiExceptionHandler();
        app.MapControllers();

        app.Init(x =>
        {
            x.InitPersistence();
        });

        app.Run();
    }
}
