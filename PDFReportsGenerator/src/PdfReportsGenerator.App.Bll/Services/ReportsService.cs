using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.App.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal;
using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.App.Bll.Services;

public class ReportsService : IReportsService
{
    private readonly ApplicationDbContext _dbContext;

    public ReportsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Report[]> GetReports()
    {
        return await _dbContext.Reports
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task DeleteReport(Guid id)
    {
        var entityToDelete = new Report { Id = id };
        _dbContext.Reports.Attach(entityToDelete);
        _dbContext.Reports.Remove(entityToDelete);

        await _dbContext.SaveChangesAsync();
    }
    
    // TODO: Produce message to Kafka here.
    public Task RegenerateReport(Guid id)
    {
        throw new NotImplementedException();
    }
}