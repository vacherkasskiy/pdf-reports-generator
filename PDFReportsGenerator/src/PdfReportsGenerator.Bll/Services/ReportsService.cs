using FluentValidation;
using Newtonsoft.Json;
using PdfReportsGenerator.Bll.Exceptions;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal;
using Report = PdfReportsGenerator.Dal.Entities.Report;

namespace PdfReportsGenerator.Bll.Services;

public class ReportsService : IReportsService
{
    private readonly IValidator<Models.Report> _validator;
    private readonly ApplicationDbContext _dbContext;
    
    public ReportsService(
        IValidator<Models.Report> validator, 
        ApplicationDbContext dbContext)
    {
        _validator = validator;
        _dbContext = dbContext;
    }
    
    public async Task<Report> CreateReport(Models.Report report)
    {
        var result = await _validator.ValidateAsync(report);
        if (!result.IsValid) 
            throw new InvalidReportFormatException("Report with invalid format was provided");
        
        var entityEntry = await _dbContext.Reports.AddAsync(new ()
        {
            Body = JsonConvert.SerializeObject(report)
        });
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }
}