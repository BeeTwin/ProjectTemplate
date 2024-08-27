namespace Infrastructure.Environment;

public static class EnvironmentVariables
{
    private static string? _postgresConnectionString;
    public static string PostgresConnectionString =>
        _postgresConnectionString ??= GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

    private static string? _jwtSecurityKey;
    public static string JwtSecurityKey =>
        _jwtSecurityKey ??= GetEnvironmentVariable("JWT_SECURITY_KEY");

    private static string GetEnvironmentVariable(string variableName)
    {
        var variableValue = System.Environment.GetEnvironmentVariable(variableName);
        return string.IsNullOrWhiteSpace(variableValue)
            ? throw new EmptyEnvironmentVariableException(variableName)
            : variableValue;
    }

    private sealed class EmptyEnvironmentVariableException(string variableName)
        : Exception($"Empty environment variable \"{variableName}\".");
}
