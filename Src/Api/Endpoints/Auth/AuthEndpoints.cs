using Infrastructure.Auth.Services.Login;
using Infrastructure.Auth.Services.Registration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Template.Endpoints.Auth.Requests;
using Template.Endpoints.Auth.Responses;

namespace Template.Endpoints.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        var authGroup = builder.MapGroup("")
            .WithTags(Tags.Auth)
            .AllowAnonymous();

        authGroup.MapPost("/register", Register,
            "Registers a new user account.");
        authGroup.MapPost("/login", Login,
            "Authenticates a user and generates a token.");

        return builder;
    }

    private static async Task<Results<NoContent, ValidationProblem>> Register(
        [FromBody] RegistrationRequest request,
        [FromServices] IRegistrationService registrationService)
    {
        var result = await registrationService.RegisterAsync(request.Username, request.Password);
        if (!result.Succeeded)
            return result.ToValidationProblem();

        return TypedResults.NoContent();
    }


    private static async Task<Results<Ok<TokenResponse>, ProblemHttpResult>> Login(
        [FromBody] LoginRequest request,
        [FromServices] ILoginService loginService)
    {
        var accessToken = await loginService.LoginAsync(request.Username, request.Password);
        if (accessToken is null)
            return TypedResults.Problem(new ProblemDetails
            {
                Title = "Login failed",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "Username or password is invalid."
            });

        return TypedResults.Ok(new TokenResponse(accessToken));
    }
}
