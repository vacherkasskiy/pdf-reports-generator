using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PdfReportsGenerator.Application.Exceptions;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Core;
using PdfReportsGenerator.Core.Entities;
using PdfReportsGenerator.Core.Models;
using PDFReportsGenerator.Kafka;

namespace PdfReportsGenerator.Application.Services;

public class ReportsService : IReportsService
{
    private readonly IValidator<ReportBody> _validator;
    private readonly ApplicationDbContext _dbContext;
    private readonly KafkaProducer _kafkaProducer = new ();
    
    public ReportsService(
        IValidator<ReportBody> validator, 
        ApplicationDbContext dbContext)
    {
        _validator = validator;
        _dbContext = dbContext;
    }
    
    public async Task<ReportTask> CreateReport(ReportBody report)
    {
        var result = await _validator.ValidateAsync(report);
        if (!result.IsValid)
        {
            var errors = new StringBuilder();
            for (var i = 0; i < result.Errors.Count; ++i)
            {
                errors.Append($"{i + 1}. {result.Errors[i].ErrorMessage}\n");
            }
            
            throw new InvalidReportFormatException($"Report with invalid format was provided. Errors are\n {errors}");
        }
        
        var entity = (await _dbContext.ReportTasks.AddAsync(new ReportTask
        {
            Status = ReportStatus.NotStarted,
            ReportBody = JsonConvert.SerializeObject(report)
        })).Entity;
        
        await _dbContext.SaveChangesAsync();
        
        var body = JsonConvert.SerializeObject(new KafkaRecord
        {
            TaskId = entity.Id, 
            ReportBody = report
        });
        await _kafkaProducer.Produce(entity.Id.ToString(), body);

        return entity;
    }

    public async Task<ReportTask> GetReport(string reportGuid)
    {
        try
        {
            var report = await _dbContext.ReportTasks
                .AsNoTracking()
                .SingleAsync(x => x.Id == Guid.Parse(reportGuid));

            return report;
        }
        catch
        {
            throw new ReportNotFoundException("No report was found by provided id");
        }
    }

    public async Task<ReportTask> UpdateReport(ReportTask report)
    {
        var result = _dbContext.ReportTasks.Update(report);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        return result.Entity;
    }
}