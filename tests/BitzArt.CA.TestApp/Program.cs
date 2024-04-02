using BitzArt.ApiExceptions.AspNetCore;

namespace BitzArt.CA.Infrastructure.AspNetCore.TestApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApiExceptionHandler(x =>
        {
            x.DisplayInnerExceptions = true;
            x.LogExceptions = true;
            x.LogRequests = true;
        });
        builder.Services.ConfigureDefaultHttpJsonOptions();
        builder.Services.AddControllers().AddDefaultJsonOptions();

        var app = builder.Build();

        app.UseApiExceptionHandler();
        app.MapControllers();

        app.Run();
    }
}
