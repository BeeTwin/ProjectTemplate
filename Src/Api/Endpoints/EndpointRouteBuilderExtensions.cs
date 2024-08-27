using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Template.Endpoints;

public static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder MapGet(
        this RouteGroupBuilder builder,
        [StringSyntax("Route"), RouteTemplate] string pattern,
        Delegate handler,
        string summary,
        [CallerArgumentExpression(nameof(handler))] string handlerName = "") =>
        builder.MapAny(HttpMethods.Get, pattern, handler, summary, handlerName);

    public static RouteHandlerBuilder MapPost(
        this RouteGroupBuilder builder,
        [StringSyntax("Route"), RouteTemplate] string pattern,
        Delegate handler,
        string summary,
        [CallerArgumentExpression(nameof(handler))] string handlerName = "") =>
        builder.MapAny(HttpMethods.Post, pattern, handler, summary, handlerName);

    public static RouteHandlerBuilder MapDelete(
        this RouteGroupBuilder builder,
        [StringSyntax("Route"), RouteTemplate] string pattern,
        Delegate handler,
        string summary,
        [CallerArgumentExpression(nameof(handler))] string handlerName = "") =>
        builder.MapAny(HttpMethods.Delete, pattern, handler, summary, handlerName);

    private static RouteHandlerBuilder MapAny(
        this RouteGroupBuilder builder,
        string methodName,
        string pattern,
        Delegate handler,
        string summary,
        string handlerName) =>
        builder.MapMethods(pattern, [methodName], handler)
            .WithOpenApi()
            .WithName(handlerName)
            .WithDisplayName(handlerName)
            .WithSummary(summary);
}
