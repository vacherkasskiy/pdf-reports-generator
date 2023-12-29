using Newtonsoft.Json;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Bll.Validators.Interfaces;
using PdfReportsGenerator.Dal;
using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.Bll.Services;

public class ReportTasksService : IReportTasksService
{
    private readonly IValidator<Report> _validator;
    private readonly ApplicationDbContext _dbContext;

    public ReportTasksService(IValidator<Report> validator, ApplicationDbContext dbContext)
    {
        _validator = validator;
        _dbContext = dbContext;
    }
    
    public async Task<CreateReportTask> CreateReportTask(Report report)
    {
        if (!_validator.IsValid(report)) 
            throw new InvalidDataException("Wrong report provided");
        
        var task = _dbContext.CreateReportTasks.Add(new CreateReportTask
        {
            Report = JsonConvert.SerializeObject(report)
        });
        await _dbContext.SaveChangesAsync();

        return task.Entity;
    }
}