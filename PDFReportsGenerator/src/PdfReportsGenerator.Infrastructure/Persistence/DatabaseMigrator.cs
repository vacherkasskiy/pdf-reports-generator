using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PdfReportsGenerator.Infrastructure.Persistence;

public static class DatabaseMigrator
{
    public static void MigrateDb(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PdfGeneratorDbContext>();

        try
        {
            // Not an async call on purpose.
            dbContext.Database.Migrate();
            Log.Information("Database migrated successfully.");
        }
        catch (Exception ex)
        {
            Log.Error($"Error during database migration: {ex.Message}");
            throw;
        }
    }
}