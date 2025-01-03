using PdfReportsGenerator.Application.Models.Enums;

namespace PdfReportsGenerator.Application.Infrastructure.Hubs;

public interface IPdfReportHubContext
{
    Task ReceivePdfReportTaskStatus(Guid reportTaskId, ReportStatuses reportStatus);
}