using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Dal.Entities;
using PdfReportsGenerator.Dal.Repositories.Interfaces;

namespace PdfReportsGenerator.Dal.Repositories;

public class ReportsRepository : IRepository<Report>
{
    private readonly ApplicationDbContext _dbContext;
    
    public ReportsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Report?> Get(long entityId)
    {
        try
        {
            return await _dbContext
                .Reports
                .AsNoTracking()
                .SingleAsync(x => x.Id == entityId);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Report> Add(Report entity)
    {
        var response = await _dbContext
            .Reports
            .AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return response.Entity;
    }

    public async Task Delete(Report entity)
    {
        _dbContext.Reports.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}