using Infrastructure.Auth.Tokens;

namespace Infrastructure.Auth.Services.Login;

public sealed class LoginService(UserManager<AuthUser> userManager, ITokenService tokenService) : ILoginService
{
    public async Task<string?> LoginAsync(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user is null || !await userManager.CheckPasswordAsync(user, password))
            return null;

        var userRoles = await userManager.GetRolesAsync(user);
        return tokenService.CreateToken(user, userRoles);
    }
}
