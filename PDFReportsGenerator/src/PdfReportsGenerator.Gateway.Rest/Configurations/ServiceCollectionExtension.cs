using AutoMapper;
using PdfReportsGenerator.Application.Builders;
using PdfReportsGenerator.Gateway.Rest.Builders;

namespace PdfReportsGenerator.Gateway.Rest.Configurations;

public static class ServiceCollectionExtension
{
    private const string CorsPolicyName = "AllowLocalhost3000";

    public static void ConfigureRestGateway(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName,
                config =>
                {
                    config.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithExposedHeaders("Access-Control-Allow-Origin");
                });
        });

        services.ConfigureMapper();
    }

    public static void ApplyRestGatewaySettings(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
    }

    private static void ConfigureMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ReportBuilder());
            mc.AddProfile(new ReportRequestsBuilder());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}