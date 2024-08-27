using Infrastructure.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Endpoints.Admin.Auth.Users.UserRoles.Responses;

namespace Template.Endpoints.Admin.Auth.Users.UserRoles;

public static class UserRolesEndpoints
{
    public static IEndpointRouteBuilder MapUserRolesEndpoints(this IEndpointRouteBuilder builder)
    {
        var userRolesGroup = builder.MapGroup("{userId}/roles");

        userRolesGroup.MapGet("", GetUserRoles,
            "Retrieves the list of roles assigned to the specified user.");
        userRolesGroup.MapPost("{roleId}", AddUserToRole,
            "Assigns the specified role to the user.");
        userRolesGroup.MapDelete("{roleId}", RemoveUserFromRole,
            "Removes the specified role from the user.");

        return builder;
    }

    private static async Task<Results<Ok<UserRolesResponse>, NotFound<ProblemDetails>>> GetUserRoles(
        [FromRoute] Guid userId,
        [FromServices] UserManager<AuthUser> userManager)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return CustomResults.UserNotFound(userId);

        var userRoles = await userManager.GetRolesAsync(user);

        return UserRoles(userRoles);
    }

    private static async Task<Results<Ok<UserRolesResponse>, NotFound<ProblemDetails>, ValidationProblem>> AddUserToRole(
        [FromRoute] Guid userId,
        [FromRoute] Guid roleId,
        [FromServices] UserManager<AuthUser> userManager,
        [FromServices] RoleManager<IdentityRole> roleManager)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return CustomResults.UserNotFound(userId);

        var role = await roleManager.FindByIdAsync(roleId);
        if (role is null)
            return CustomResults.RoleNotFound(roleId);

        var result = await userManager.AddToRoleAsync(user, role.Name!);
        if (!result.Succeeded)
            return result.ToValidationProblem();

        var userRoles = await userManager.GetRolesAsync(user);

        return UserRoles(userRoles);
    }

    private static async Task<Results<Ok<UserRolesResponse>, NotFound<ProblemDetails>, ValidationProblem>> RemoveUserFromRole(
        [FromRoute] Guid userId,
        [FromRoute] Guid roleId,
        [FromServices] UserManager<AuthUser> userManager,
        [FromServices] RoleManager<IdentityRole> roleManager)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return CustomResults.UserNotFound(userId);

        var role = await roleManager.FindByIdAsync(roleId);
        if (role is null)
            return CustomResults.RoleNotFound(roleId);

        var result = await userManager.RemoveFromRoleAsync(user, role.Name!);
        if (!result.Succeeded)
            return result.ToValidationProblem();

        var userRoles = await userManager.GetRolesAsync(user);

        return UserRoles(userRoles);
    }

    private static Ok<UserRolesResponse> UserRoles(IEnumerable<string> userRoles) =>
        TypedResults.Ok(new UserRolesResponse(userRoles));
}
