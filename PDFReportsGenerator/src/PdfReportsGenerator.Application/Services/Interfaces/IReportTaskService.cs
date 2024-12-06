using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Services.Interfaces;

public interface IReportTaskService
{
    Task<Guid> CreateReportAsync(ReportTaskDto report);
    
    Task<ReportTaskDto> GetReportAsync(string reportGuid);
    
    Task<ReportTaskDto> UpdateReportAsync(ReportTaskDto report);
}