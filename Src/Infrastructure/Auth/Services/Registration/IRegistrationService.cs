namespace Infrastructure.Auth.Services.Registration;

public interface IRegistrationService
{
    Task<IdentityResult> RegisterAsync(string username, string password);
}
