using PdfReportsGenerator.Application.Exceptions;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Infrastructure.Persistence.Repositories;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Models.Enums;
using PdfReportsGenerator.Application.Services.Interfaces;

namespace PdfReportsGenerator.Application.Services;

public class ReportTaskService : IReportTaskService
{
    private readonly IReportTaskRepository _repository;
    private readonly IKafkaProducer _kafkaProducer;
    
    public ReportTaskService(IReportTaskRepository taskRepository, IKafkaProducer kafkaProducer)
    {
        _repository = taskRepository;
        _kafkaProducer = kafkaProducer;
    }
    
    public async Task<Guid> CreateReportAsync(ReportTaskDto report)
    {
        var enrichedReport = report with
        {
            Id = Guid.NewGuid(),
            Status = ReportStatusEnum.NotStarted,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        await _repository.AddAsync(enrichedReport);
        await _kafkaProducer.ProduceAsync(enrichedReport);

        return enrichedReport.Id;
    }

    public async Task<ReportTaskDto> GetReportAsync(string reportGuid)
    {
        try
        {
            var report = await _repository.GetByIdAsync(Guid.Parse(reportGuid));

            return report;
        }
        catch
        {
            throw new ReportNotFoundException("No report was found by provided id");
        }
    }

    public async Task<ReportTaskDto> UpdateReportAsync(ReportTaskDto report)
    {
        var result = await _repository.UpdateAsync(report);

        return result;
    }
}