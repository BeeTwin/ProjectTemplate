using System.Text;
using Infrastructure.Auth;
using Infrastructure.Auth.Policies;
using Infrastructure.Auth.Services.Login;
using Infrastructure.Auth.Services.Registration;
using Infrastructure.Auth.Tokens;
using Infrastructure.Environment;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SimpleOptions.Configuration;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(EnvironmentVariables.PostgresConnectionString));
        builder.AddAuth();

        return builder;
    }

    private static IHostApplicationBuilder AddAuth(this IHostApplicationBuilder builder)
    {
        builder.AddConfigurationOptions<JwtOptions>(out var jwtSettings);

        builder.Services.AddIdentityCore<AuthUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JwtSecurityKey))
                };
            });
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(Policies.Admin, policyOptions =>
                policyOptions.RequireRole(Roles.Admin))
            .AddPolicy(Policies.Default, policyOptions =>
                policyOptions.RequireRole(Roles.Default));

        builder.Services.AddScoped<ITokenService, JwtTokenService>();
        builder.Services.AddScoped<IRegistrationService, RegistrationService>();
        builder.Services.AddScoped<ILoginService, LoginService>();

        return builder;
    }

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
