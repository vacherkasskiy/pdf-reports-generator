using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Application.Helpers;
using PdfReportsGenerator.Application.Helpers.Interfaces;
using PdfReportsGenerator.Application.Infrastructure.Hubs;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Infrastructure.Minio;
using PdfReportsGenerator.Application.Infrastructure.PdfGenerator;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Infrastructure.Hubs;
using PdfReportsGenerator.Infrastructure.Kafka;
using PdfReportsGenerator.Infrastructure.Kafka.Options;
using PdfReportsGenerator.Infrastructure.Minio;
using PdfReportsGenerator.Infrastructure.Minio.Options;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Helpers;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;
using PdfReportsGenerator.Infrastructure.Persistence;
using PdfReportsGenerator.Infrastructure.Persistence.Options;
using Prometheus;

namespace PdfReportsGenerator.Infrastructure.Configurations;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.ConfigureDatabase(configuration);
        services.ConfigureMessageBroker(configuration);
        services.ConfigureSignalR();
        services.ConfigureMinio(configuration);
        services.ConfigurePdfGenerator();

        return services;
    }

    public static void AddSignalR(this IEndpointRouteBuilder app)
    {
        app.MapHub<PdfReportHub>("/signalR");
    }
    
    public static IApplicationBuilder AddPrometheus(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseHttpMetrics();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapMetrics();
        });

        return app;
    }

    #region Private Members

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));

        var databaseOptions = configuration
            .GetSection(nameof(DatabaseOptions))
            .Get<DatabaseOptions>();

        if (databaseOptions == null)
        {
            throw new InvalidOperationException("DatabaseOptions is not configured.");
        }

        services.AddDbContext<IPdfGeneratorDbContext, PdfGeneratorDbContext>(options =>
            options.UseNpgsql(databaseOptions.ConnectionString));
    }

    private static void ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaConfigurationOptions>(configuration.GetSection(nameof(KafkaConfigurationOptions)));

        var brokerOptions = configuration
            .GetSection(nameof(KafkaConfigurationOptions))
            .Get<KafkaConfigurationOptions>();

        if (brokerOptions == null)
        {
            throw new InvalidOperationException("BrokerOptions is not configured.");
        }

        services.AddScoped<IKafkaProducer, KafkaProducer>();
        services.AddScoped<IKafkaMessagesHandler, KafkaMessagesHandler>();

        // Add consumer via MassTransit.
        services.AddMassTransit(x =>
        {
            x.UsingInMemory();

            x.AddRider(rider =>
            {
                rider.AddConsumer<KafkaReportsConsumer>();
                rider.AddProducer<ReportTaskDto>(brokerOptions.TopicName);

                rider.UsingKafka((context, k) =>
                {
                    k.Host(brokerOptions.Url);

                    k.TopicEndpoint<ReportTaskDto>(brokerOptions.TopicName, brokerOptions.ConsumerGroupId,
                        e => { e.ConfigureConsumer<KafkaReportsConsumer>(context); });
                });
            });
        });
    }

    private static void ConfigureSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<IPdfReportHubContext, PdfReportHubContext>();
    }

    private static void ConfigureMinio(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioConfigurationOptions>(configuration.GetSection(nameof(MinioConfigurationOptions)));
        services.AddSingleton<IPdfReportMinioClient, PdfReportMinioClient>();
    }

    private static void ConfigurePdfGenerator(this IServiceCollection services)
    {
        services.AddScoped<IPdfGenerator, PdfGenerator.PdfGenerator>();
        services.AddScoped<IPdfImageProvider, PdfImageProvider>();
        services.AddScoped<IPdfParser, PdfParser>();
    }

    #endregion
}