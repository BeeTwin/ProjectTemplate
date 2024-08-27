using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Template.Endpoints.Admin.Auth;

public static class CustomResults
{
    public static NotFound<ProblemDetails> UserNotFound(Guid userId) =>
        NotFoundById("User", userId);

    public static NotFound<ProblemDetails> RoleNotFound(Guid roleId) =>
        NotFoundById("Role", roleId);

    private static NotFound<ProblemDetails> NotFoundById(string entity, Guid id) =>
        TypedResults.NotFound(new ProblemDetails
        {
            Title = $"{entity} not found",
            Detail = $"{entity} with ID '{id}' does not exist."
        });
}
