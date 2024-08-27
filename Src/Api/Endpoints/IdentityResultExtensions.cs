using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Template.Endpoints;

public static class IdentityResultExtensions
{
    public static ValidationProblem ToValidationProblem(this IdentityResult result) =>
        TypedResults.ValidationProblem(result.Errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Description)
                    .ToArray()));
}
