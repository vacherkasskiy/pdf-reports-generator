using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Core.Entities;

namespace PdfReportsGenerator.Core;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<ReportTask> ReportTasks { get; set; }
}