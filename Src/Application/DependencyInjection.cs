using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(ApplicationAssemblyMarker.Assembly));
        builder.Services.AddValidatorsFromAssembly(ApplicationAssemblyMarker.Assembly);

        return builder;
    }
}
