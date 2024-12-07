using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
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

        services.AddScoped<IPdfGeneratorDbContext, PdfGeneratorDbContext>();
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(
            configuration.GetSection(nameof(DatabaseOptions)));
        
        var databaseOptions = configuration
            .GetSection(nameof(DatabaseOptions))
            .Get<DatabaseOptions>();

        if (string.IsNullOrEmpty(databaseOptions?.ConnectionString))
        {
            throw new InvalidOperationException("Connection string for DatabaseOptions is not configured.");
        }
        
        services.AddDbContext<PdfGeneratorDbContext>(options =>
            options.UseNpgsql(databaseOptions.ConnectionString));
    }

    private static void ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaConfigurationOptions>(
            configuration.GetSection(nameof(KafkaConfigurationOptions)));

        services.AddScoped<IKafkaProducer, KafkaProducer>();
        services.AddScoped<IKafkaConsumer, KafkaConsumer>();
    }
}
