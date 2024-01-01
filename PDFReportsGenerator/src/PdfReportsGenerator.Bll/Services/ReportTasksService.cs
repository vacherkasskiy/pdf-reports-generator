using Newtonsoft.Json;
using PdfReportsGenerator.Bll.Exceptions;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Bll.Validators.Interfaces;
using PdfReportsGenerator.Dal.Entities;
using PdfReportsGenerator.Dal.Repositories.Interfaces;

namespace PdfReportsGenerator.Bll.Services;

public class ReportTasksService : IReportTasksService
{
    private readonly IValidator<Report> _validator;
    private readonly IRepository<CreateReportTask> _repository;

    public ReportTasksService(IValidator<Report> validator, IRepository<CreateReportTask> repository)
    {
        _validator = validator;
        _repository = repository;
    }
    
    public async Task<CreateReportTask> CreateReportTask(Report report)
    {
        if (!_validator.IsValid(report)) 
            throw new InvalidReportFormatException();
        
        return await _repository.Add(new ()
        {
            Report = JsonConvert.SerializeObject(report)
        });
    }
}