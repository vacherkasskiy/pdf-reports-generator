using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PdfReportsGenerator.Bll.Exceptions;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal;
using PdfReportsGenerator.Dal.Entities;
using Report = PdfReportsGenerator.Dal.Entities.Report;

namespace PdfReportsGenerator.Bll.Services;

public class ReportsService : IReportsService
{
    private readonly IValidator<Models.Report> _validator;
    private readonly ApplicationDbContext _dbContext;
    private readonly IKafkaProducer _kafkaProducer;
    
    public ReportsService(
        IValidator<Models.Report> validator, 
        ApplicationDbContext dbContext,
        IKafkaProducer kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
        _validator = validator;
        _dbContext = dbContext;
    }
    
    public async Task<Report> CreateReport(Models.Report report)
    {
        var result = await _validator.ValidateAsync(report);
        if (!result.IsValid) 
            throw new InvalidReportFormatException("Report with invalid format was provided");

        string body = JsonConvert.SerializeObject(report);
        await _kafkaProducer.Produce(body);
        
        var entityEntry = await _dbContext.Reports.AddAsync(new Report
        {
            Status = ReportStatus.Pending
        });
        await _dbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<Report> GetReport(string reportGuid)
    {
        try
        {
            var report = await _dbContext.Reports
                .AsNoTracking()
                .SingleAsync(x => x.Id == Guid.Parse(reportGuid));

            return report;
        }
        catch
        {
            throw new ReportNotFoundException("No report was found by provided id");
        }
    }
}