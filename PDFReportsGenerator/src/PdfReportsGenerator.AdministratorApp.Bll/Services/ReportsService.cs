using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PdfReportsGenerator.AdministratorApp.Bll.Services.Interfaces;
using PdfReportsGenerator.Core;
using PdfReportsGenerator.Core.Entities;
using PdfReportsGenerator.Core.Models;
using PDFReportsGenerator.Kafka;

namespace PdfReportsGenerator.AdministratorApp.Bll.Services;

public class ReportsService : IReportsService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly KafkaProducer _kafkaProducer = new ();

    public ReportsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReportTask[]> GetReports()
    {
        return await _dbContext.ReportTasks
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task DeleteReport(string id)
    {
        var entityToDelete = new ReportTask { Id = Guid.Parse(id) };
        _dbContext.ReportTasks.Attach(entityToDelete);
        _dbContext.ReportTasks.Remove(entityToDelete);

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> RegenerateReport(string id)
    {
        var reportTask = await _dbContext.ReportTasks
            .SingleOrDefaultAsync(x => x.Id == Guid.Parse(id));

        if (reportTask == null)
        {
            return false;
        }

        reportTask.Status = ReportStatus.NotStarted;
        reportTask.Link = null;
        _dbContext.ReportTasks.Update(reportTask);
        await _dbContext.SaveChangesAsync();
        
        var body = JsonConvert.SerializeObject(new KafkaRecord
        {
            TaskId = reportTask.Id, 
            ReportBody = JsonConvert.DeserializeObject<ReportBody>(reportTask.ReportBody)!
        });
        await _kafkaProducer.Produce(reportTask.Id.ToString(), body);

        return true;
    }
}