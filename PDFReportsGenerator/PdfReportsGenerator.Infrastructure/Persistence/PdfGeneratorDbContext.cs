using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
using PdfReportsGenerator.Domain.Entities;

namespace PdfReportsGenerator.Infrastructure.Persistence;

internal sealed class PdfGeneratorDbContext : DbContext, IPdfGeneratorDbContext
{
    public PdfGeneratorDbContext(DbContextOptions<PdfGeneratorDbContext> options) : base(options)
    {
    }

    public DbSet<ReportTask> ReportTasks { get; set; }

    public Task SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }
}