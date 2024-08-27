namespace Template.Endpoints.WeatherForecast.Responses;

public record WeatherForecastResponse(DateOnly Date, int TemperatureC, string? Summary);