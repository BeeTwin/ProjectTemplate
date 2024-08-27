using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Template.Endpoints.Admin;
using Template.Endpoints.Auth;
using Template.Endpoints.WeatherForecast;

namespace Template.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication UseSwaggerInDevelopment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapAdminEndpoints();
        app.MapAuthEndpoints();
        app.MapWeatherForecastEndpoints();

        return app;
    }

    public static async Task RunMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        await context.Database.MigrateAsync();
    }
}
