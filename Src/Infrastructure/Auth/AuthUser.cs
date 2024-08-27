using Domain.Users;

namespace Infrastructure.Auth;

public sealed class AuthUser : IdentityUser
{
    public User DomainUser { get; set; } = default!;
}
