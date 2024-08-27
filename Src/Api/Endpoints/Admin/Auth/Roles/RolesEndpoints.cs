using Infrastructure.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template.Endpoints.Admin.Auth.Roles.Mapping;
using Template.Endpoints.Admin.Auth.Roles.Requests;
using Template.Endpoints.Admin.Auth.Roles.Responses;
using Template.Endpoints.Admin.Auth.Roles.RoleUsers;

namespace Template.Endpoints.Admin.Auth.Roles;

public static class RolesEndpoints
{
    public static IEndpointRouteBuilder MapRolesEndpoints(this IEndpointRouteBuilder builder)
    {
        var rolesGroup = builder.MapGroup("roles");

        rolesGroup.MapGet("", GetRoles,
            "Retrieves the list of all roles.");
        rolesGroup.MapPost("", AddRole,
            "Creates and adds a new role.");
        rolesGroup.MapDelete("{roleId}", DeleteRole,
            "Removes the specified role.");

        rolesGroup.MapRoleUsersEndpoints();

        return builder;
    }

    private static async Task<Ok<RolesResponse>> GetRoles(
        [FromServices] RoleManager<IdentityRole> roleManager,
        CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.ToListAsync(cancellationToken);
        return TypedResults.Ok(new RolesResponse(roles.ToRoleDtos()));
    }

    private static async Task<Results<CreatedAtRoute, ValidationProblem>> AddRole(
        [FromBody] AddRoleRequest request,
        [FromServices] RoleManager<IdentityRole> roleManager)
    {
        var result = await roleManager.CreateAsync(new IdentityRole(request.RoleName));
        if (!result.Succeeded)
            return result.ToValidationProblem();

        return TypedResults.CreatedAtRoute(nameof(AddRole), null);
    }

    private static async Task<Results<NoContent, NotFound<ProblemDetails>, ValidationProblem>> DeleteRole(
        [FromRoute] Guid roleId,
        [FromServices] RoleManager<IdentityRole> roleManager,
        CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(roleId);
        if (role is null)
            return CustomResults.RoleNotFound(roleId);

        var result = await roleManager.DeleteAsync(role);
        if (!result.Succeeded)
            return result.ToValidationProblem();

        return TypedResults.NoContent();
    }
}
