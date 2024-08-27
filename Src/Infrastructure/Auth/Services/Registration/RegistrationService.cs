namespace Infrastructure.Auth.Services.Registration;

public sealed class RegistrationService(UserManager<AuthUser> userManager) : IRegistrationService
{
    public async Task<IdentityResult> RegisterAsync(string username, string password)
    {
        const int minimalUsernameLength = 5;
        if (username is not { Length: >= minimalUsernameLength })
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UsernameIsTooShort",
                Description = $"Usernames must be at least {minimalUsernameLength} characters."
            });

        var authUser = new AuthUser
        {
            UserName = username,
            DomainUser = new()
        };

        var result = await userManager.CreateAsync(authUser, password);
        if (!result.Succeeded)
            return result;

        return await userManager.AddToRoleAsync(authUser, Roles.Default);
    }
}
