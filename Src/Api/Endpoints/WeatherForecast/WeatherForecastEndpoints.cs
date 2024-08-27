using System.IO.Compression;
using System.Text;
using Infrastructure.Auth.Policies;
using Microsoft.AspNetCore.Http.HttpResults;
using Template.Endpoints.WeatherForecast.Responses;

namespace Template.Endpoints.WeatherForecast;

public static class WeatherForecastEndpoints
{
    private static readonly string[] Summaries =
        ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public static IEndpointRouteBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder builder)
    {
        var weatherGroup = builder.MapGroup("weatherforecast")
            .WithTags(Tags.Weather)
            .RequireDefault();

        weatherGroup.MapGet("", GetWeatherForecast,
            "Retrieves the 5-day weather forecast.");

        return builder;
    }

    private static Ok<WeatherForecastResponse[]> GetWeatherForecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastResponse
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();
        return TypedResults.Ok(forecast);
    }
}
