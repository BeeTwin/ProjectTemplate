namespace Infrastructure.Auth;

public static class ManagerExtensions
{
    public static Task<TUser?> FindByIdAsync<TUser>(this UserManager<TUser> userManager, Guid roleId)
        where TUser : class =>
        userManager.FindByIdAsync(roleId.ToString());

    public static Task<TRole?> FindByIdAsync<TRole>(this RoleManager<TRole> roleManager, Guid roleId)
        where TRole : class =>
        roleManager.FindByIdAsync(roleId.ToString());
}
