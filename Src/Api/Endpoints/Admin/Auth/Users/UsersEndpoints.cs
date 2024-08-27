using Infrastructure.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template.Endpoints.Admin.Auth.Users.Mapping;
using Template.Endpoints.Admin.Auth.Users.Responses;
using Template.Endpoints.Admin.Auth.Users.UserRoles;

namespace Template.Endpoints.Admin.Auth.Users;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder builder)
    {
        var usersGroup = builder.MapGroup("users");

        usersGroup.MapGet("", GetUsers,
            "Retrieves the list of all registered users.");
        usersGroup.MapDelete("{userId}", DeleteUser,
            "Removes the specified user.");

        usersGroup.MapUserRolesEndpoints();

        return builder;
    }

    private static async Task<Ok<UsersResponse>> GetUsers(
        [FromServices] UserManager<AuthUser> userManager,
        CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        return TypedResults.Ok(new UsersResponse(users.Count, users.ToUserDtos()));
    }

    private static async Task<Results<NoContent, NotFound<ProblemDetails>, ValidationProblem>> DeleteUser(
        [FromRoute] Guid userId,
        [FromServices] UserManager<AuthUser> userManager)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return CustomResults.UserNotFound(userId);

        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return result.ToValidationProblem();

        return TypedResults.NoContent();
    }
}
