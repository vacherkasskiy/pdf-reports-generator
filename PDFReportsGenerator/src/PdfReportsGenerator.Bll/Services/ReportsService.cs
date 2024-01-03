using FluentValidation;
using Newtonsoft.Json;
using PdfReportsGenerator.Bll.Exceptions;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal.Repositories.Interfaces;
using Report = PdfReportsGenerator.Dal.Entities.Report;

namespace PdfReportsGenerator.Bll.Services;

public class ReportsService : IReportsService
{
    private readonly IValidator<Models.Report> _validator;
    private readonly IRepository<Report> _repository;

    public ReportsService(IValidator<Models.Report> validator, IRepository<Report> repository)
    {
        _validator = validator;
        _repository = repository;
    }
    
    public async Task<Report> CreateReportTask(Models.Report report)
    {
        var result = await _validator.ValidateAsync(report);
        if (!result.IsValid) 
            throw new InvalidReportFormatException();
        
        return await _repository.Add(new ()
        {
            Body = JsonConvert.SerializeObject(report)
        });
    }
}