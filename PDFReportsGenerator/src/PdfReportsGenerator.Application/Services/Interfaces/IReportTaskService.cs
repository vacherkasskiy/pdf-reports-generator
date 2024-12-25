using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Models.Enums;

namespace PdfReportsGenerator.Application.Services.Interfaces;

public interface IReportTaskService
{
    Task<Guid> CreateReportAsync(ReportTaskDto report);

    Task<ReportTaskDto> GetReportAsync(string reportId);

    Task SetStatusToReportAsync(Guid reportId, ReportStatuses status, string? reportLink = null);

    Task DownloadReportAsync(string fileName, Stream destinationStream);
}