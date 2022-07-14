namespace System.WebAPI;

using System.EntityFramework.Data;
using System.Models;
using System.Models.Model;
using System.Text;
using System.Utils.Auth;
using System.WebAPI.ConfigureOptions;
using System.WebAPI.Options;
using Boxed.AspNetCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
/// </summary>
internal static class CustomServiceCollectionExtensions
{
    public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidation(
                x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Singleton);
                    x.DisableDataAnnotationsValidation = true;
                });

    /// <summary>
    /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
    /// Objects (POCO) and adding <see cref="IOptions{T}"/> objects to the services collection.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services with options services added.</returns>
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            // ConfigureAndValidateSingleton registers IOptions<T> and also T as a singleton to the services collection.
            .ConfigureAndValidateSingleton<ApplicationOptions>(configuration)
            .ConfigureAndValidateSingleton<CacheProfileOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.CacheProfiles)))
            .ConfigureAndValidateSingleton<CompressionOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Compression)))
            .ConfigureAndValidateSingleton<ForwardedHeadersOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.ForwardedHeaders)))
            .Configure<ForwardedHeadersOptions>(
                options =>
                {
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                })
            .ConfigureAndValidateSingleton<HostOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Host)))
            .ConfigureAndValidateSingleton<KestrelServerOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Kestrel)));

    public static IServiceCollection AddCustomConfigureOptions(this IServiceCollection services) =>
        services
            .ConfigureOptions<ConfigureApiVersioningOptions>()
            .ConfigureOptions<ConfigureMvcOptions>()
            .ConfigureOptions<ConfigureCorsOptions>()
            .ConfigureOptions<ConfigureJsonOptions>()
            .ConfigureOptions<ConfigureResponseCompressionOptions>()
            .ConfigureOptions<ConfigureRouteOptions>()
            .ConfigureOptions<ConfigureSwaggerGenOptions>()
            .ConfigureOptions<ConfigureSwaggerUIOptions>()
            .ConfigureOptions<ConfigureStaticFileOptions>();

    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services) =>
        services
            .AddHealthChecks()
            // Add health checks for external dependencies here. See https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
            .Services;

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        var connection = configuration.GetSection("ConnectionStrings");
        services.Configure<ConnectionString>(connection);

        var connectionString = connection.Get<ConnectionString>();
        // Admin
        services.AddDbContext<AdminDbContext>(options => options.UseSqlServer(connectionString.AdminManagerConnection, b => b.MigrationsAssembly("System.WebAPI")));
        services.AddDbContext<GiftCodeDbContext>(options => options.UseSqlServer(connectionString.BillingGifCodeConnection, b => b.MigrationsAssembly("System.WebAPI")));
        return services;
    }

    public static IServiceCollection ConfigureJwtToken(this IServiceCollection services, IConfiguration configuration)
    {

        var jwtConfig = configuration.GetSection("JwtToken");
        services.Configure<AuthenToken>(jwtConfig);

        var jwtToken = jwtConfig.Get<AuthenToken>();

        var key = Encoding.ASCII.GetBytes(jwtToken.Key);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };
        });

        return services;
    }

    //public static IServiceCollection AddConfigureService(this IServiceCollection services)
    //{
    //    services.AddSingleton<JwtTokenServices>();
    //    return services;
    //}
}
