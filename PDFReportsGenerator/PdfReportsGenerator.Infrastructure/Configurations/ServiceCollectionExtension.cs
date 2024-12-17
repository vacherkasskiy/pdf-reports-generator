using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Infrastructure.Kafka;
using PdfReportsGenerator.Infrastructure.Kafka.Options;
using PdfReportsGenerator.Infrastructure.Persistence;
using PdfReportsGenerator.Infrastructure.Persistence.Options;

namespace PdfReportsGenerator.Infrastructure.Configurations;

public static class ServiceCollectionExtension
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase(configuration);
        services.ConfigureMessageBroker(configuration);
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(
            configuration.GetSection(nameof(DatabaseOptions)));
        
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
        services.Configure<KafkaConfigurationOptions>(
            configuration.GetSection(nameof(KafkaConfigurationOptions)));
        
        var brokerOptions = configuration
            .GetSection(nameof(KafkaConfigurationOptions))
            .Get<KafkaConfigurationOptions>();
        
        if (brokerOptions == null)
        {
            throw new InvalidOperationException("BrokerOptions is not configured.");
        }

        services.AddScoped<IKafkaProducer, KafkaProducer>();
        
        // Add consumer via MassTransit.
        services.AddMassTransit(x =>
        {
            x.UsingInMemory();

            x.AddRider(rider =>
            {
                rider.AddConsumer<KafkaReportsConsumer>();

                rider.UsingKafka((context, k) =>
                {
                    k.Host(brokerOptions.KafkaExternalAddress);

                    k.TopicEndpoint<ReportTaskDto>(brokerOptions.TopicName, brokerOptions.ConsumerGroupId, e =>
                    {
                        e.ConfigureConsumer<KafkaReportsConsumer>(context);
                    });
                });
            });
        });
    }
}
