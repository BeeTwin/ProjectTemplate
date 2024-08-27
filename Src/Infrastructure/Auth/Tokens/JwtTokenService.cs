using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Environment;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth.Tokens;

public sealed class JwtTokenService(IOptions<JwtOptions> jwtSettingsOptions) : ITokenService
{
    private readonly JwtOptions _jwtOptions = jwtSettingsOptions.Value;

    public string CreateToken(AuthUser user, IEnumerable<string> roles)
    {
        var token = CreateJwtToken(
            CreateClaims(user, roles),
            CreateSigningCredentials(),
            DateTime.UtcNow.Add(_jwtOptions.TokenLifespan));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) =>
        new(_jwtOptions.ValidIssuer,
            _jwtOptions.ValidAudience,
            claims,
            expires: expiration,
            signingCredentials: credentials);

    private static List<Claim> CreateClaims(AuthUser user, IEnumerable<string> roles)
    {
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName!)
        ];

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private static SigningCredentials CreateSigningCredentials() =>
        new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JwtSecurityKey)), SecurityAlgorithms.HmacSha256);
}
