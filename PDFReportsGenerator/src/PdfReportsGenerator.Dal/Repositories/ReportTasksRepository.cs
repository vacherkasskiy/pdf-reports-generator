using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Dal.Entities;
using PdfReportsGenerator.Dal.Repositories.Interfaces;

namespace PdfReportsGenerator.Dal.Repositories;

public class ReportTasksRepository : IRepository<CreateReportTask>
{
    private readonly ApplicationDbContext _dbContext;
    
    public ReportTasksRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CreateReportTask?> Get(long entityId)
    {
        try
        {
            return await _dbContext
                .CreateReportTasks
                .SingleAsync(x => x.Id == entityId);
        }
        catch
        {
            return null;
        }
    }

    public async Task<CreateReportTask> Add(CreateReportTask entity)
    {
        var response = await _dbContext
            .CreateReportTasks
            .AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return response.Entity;
    }

    public async Task Delete(long entityId)
    {
        var entity = await Get(entityId);
        if (entity != null)
        {
            _dbContext.CreateReportTasks.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}