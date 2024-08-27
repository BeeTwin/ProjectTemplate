namespace Infrastructure.Auth.Tokens;

public interface ITokenService
{
    string CreateToken(AuthUser user, IEnumerable<string> roles);
}
