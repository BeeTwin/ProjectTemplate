using SimpleOptions;

namespace Infrastructure.Auth;

public sealed class JwtOptions : ConfigurationOptions<JwtOptions>
{
    public string ValidIssuer { get; init; } = string.Empty;
    public string ValidAudience { get; init; } = string.Empty;
    public int TokenLifeInDays { get; init; } = 30;
    public TimeSpan TokenLifespan => TimeSpan.FromDays(TokenLifeInDays);
}
