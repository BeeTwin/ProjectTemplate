namespace Infrastructure.Auth.Services.Login;

public interface ILoginService
{
    Task<string?> LoginAsync(string username, string password);
}
