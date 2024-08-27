using Infrastructure.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Endpoints.Admin.Auth.Roles.RoleUsers.Mapping;
using Template.Endpoints.Admin.Auth.Roles.RoleUsers.Responses;

namespace Template.Endpoints.Admin.Auth.Roles.RoleUsers;

public static class RoleUsersEndpoints
{
    public static IEndpointRouteBuilder MapRoleUsersEndpoints(this IEndpointRouteBuilder builder)
    {
        var roleUsersGroup = builder.MapGroup("{roleId}/users");

        roleUsersGroup.MapGet("", GetRoleUsers,
            "Retrieves a list of users assigned to a specified role.");

        return builder;
    }

    private static async Task<Results<Ok<RoleUsersResponse>, NotFound<ProblemDetails>>> GetRoleUsers(
        [FromRoute] Guid roleId,
        [FromServices] RoleManager<IdentityRole> roleManager,
        [FromServices] UserManager<AuthUser> userManager)
    {
        var role = await roleManager.FindByIdAsync(roleId);
        if (role is null)
            return CustomResults.RoleNotFound(roleId);

        var users = await userManager.GetUsersInRoleAsync(role.Name!);
        return TypedResults.Ok(new RoleUsersResponse(users.Count, users.ToRoleUserDtos()));
    }
}
