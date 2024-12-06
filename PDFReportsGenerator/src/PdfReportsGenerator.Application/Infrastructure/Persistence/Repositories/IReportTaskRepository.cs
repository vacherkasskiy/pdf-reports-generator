using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Infrastructure.Persistence.Repositories;

public interface IReportTaskRepository
{
    Task AddAsync(ReportTaskDto report);
    
    Task<ReportTaskDto> GetByIdAsync(Guid reportId);
    
    Task<ReportTaskDto> UpdateAsync(ReportTaskDto report);
}