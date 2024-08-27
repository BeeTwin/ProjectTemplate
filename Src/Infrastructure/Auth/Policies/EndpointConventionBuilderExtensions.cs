using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Auth.Policies;

public static class EndpointConventionBuilderExtensions
{
    public static TBuilder RequireDefault<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder =>
        builder.RequireAuthorization(Policies.Default);

    public static TBuilder RequireAdmin<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder =>
        builder.RequireAuthorization(Policies.Admin);
}
