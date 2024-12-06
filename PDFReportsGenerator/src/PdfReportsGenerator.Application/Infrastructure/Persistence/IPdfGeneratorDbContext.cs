using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Domain.Entities;

namespace PdfReportsGenerator.Application.Infrastructure.Persistence;

public interface IPdfGeneratorDbContext
{
    DbSet<ReportTask> ReportTasks { get; set; }
    
    Task SaveChangesAsync();
}