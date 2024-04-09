using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PdfReportsGenerator.Bll.Exceptions;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal;
using PdfReportsGenerator.Dal.Entities;
using PDFReportsGenerator.Kafka;
using Report = PdfReportsGenerator.Dal.Entities.Report;

namespace PdfReportsGenerator.Bll.Services;

public class ReportsService : IReportsService
{
    private readonly IValidator<Models.Report> _validator;
    private readonly ApplicationDbContext _dbContext;
    private readonly KafkaProducer _kafkaProducer = new ();
    
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
        {
            string errors = "";
            for (var i = 0; i < result.Errors.Count; ++i)
                errors += $"{i + 1}. {result.Errors[i].ErrorMessage}\n";
            
            throw new InvalidReportFormatException($"Report with invalid format was provided. Errors are\n {errors}");
        }
        
        var entity = (await _dbContext.Reports.AddAsync(new Report
        {
            Status = ReportStatus.NotStarted,
            ReportBody = JsonConvert.SerializeObject(report)
        })).Entity;
        await _dbContext.SaveChangesAsync();
        
        var body = JsonConvert.SerializeObject(new KafkaRecord
        {
            TaskId = entity.Id, 
            Report = report
        });
        await _kafkaProducer.Produce(body);

        return entity;
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

    public async Task<Report> UpdateReport(Report report)
    {
        var result = _dbContext.Reports.Update(report);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }
}