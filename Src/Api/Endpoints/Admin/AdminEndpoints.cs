using Infrastructure.Auth.Policies;
using Template.Endpoints.Admin.Auth.Roles;
using Template.Endpoints.Admin.Auth.Users;

namespace Template.Endpoints.Admin;

public static class AdminEndpoints
{
    public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder builder)
    {
        var adminGroup = builder.MapGroup("admin")
            .WithTags(Tags.Admin)
            .RequireAdmin();

        adminGroup
            .MapRolesEndpoints()
            .MapUsersEndpoints();

        return builder;
    }
}
