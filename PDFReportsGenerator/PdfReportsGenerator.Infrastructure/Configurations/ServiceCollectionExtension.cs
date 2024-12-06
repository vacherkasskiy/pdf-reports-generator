using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
using PdfReportsGenerator.Infrastructure.Persistence;

namespace PdfReportsGenerator.Infrastructure.Configurations;

public static class ServiceCollectionExtension
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PdfGeneratorDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

        services.AddScoped<IPdfGeneratorDbContext, PdfGeneratorDbContext>();
    }
}