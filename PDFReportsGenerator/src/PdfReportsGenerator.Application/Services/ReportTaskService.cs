using AutoMapper;
using PdfReportsGenerator.Application.Exceptions;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Models.Enums;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Domain.Entities;

namespace PdfReportsGenerator.Application.Services;

public class ReportTaskService : IReportTaskService
{
    private readonly IPdfGeneratorDbContext _context;
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IMapper _mapper;

    public ReportTaskService(IPdfGeneratorDbContext context, IKafkaProducer kafkaProducer, IMapper mapper)
    {
        _context = context;
        _kafkaProducer = kafkaProducer;
        _mapper = mapper;
    }

    public async Task<Guid> CreateReportAsync(ReportTaskDto report)
    {
        var enrichedReport = report with
        {
            Id = Guid.NewGuid(),
            Status = ReportStatuses.NotStarted,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.ReportTasks.Add(_mapper.Map<ReportTask>(enrichedReport));
        await _context.SaveChangesAsync();
        await _kafkaProducer.ProduceAsync(enrichedReport);

        return enrichedReport.Id;
    }

    public async Task<ReportTaskDto> GetReportAsync(string reportId)
    {
        try
        {
            var report = await _context.ReportTasks.FindAsync(Guid.Parse(reportId));

            return _mapper.Map<ReportTaskDto>(report);
        }
        catch
        {
            throw new ReportNotFoundException("No report was found by provided id");
        }
    }

    public async Task SetStatusToReportAsync(Guid reportId, ReportStatuses status, string? reportLink = null)
    {
        var report = await _context.ReportTasks.FindAsync(reportId);

        if (report == null)
        {
            throw new KeyNotFoundException($"Report with ID {reportId} was not found.");
        }

        var reportDto = _mapper.Map<ReportTaskDto>(report);
        var enrichedReport = reportDto with
        {
            Status = status,
            ReportS3Link = reportLink ?? reportDto.ReportS3Link,
            UpdatedAt = DateTime.UtcNow
        };

        _mapper.Map(enrichedReport, report);

        await _context.SaveChangesAsync();
    }
}