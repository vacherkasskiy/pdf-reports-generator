using PdfReportsGenerator.Application.Models.Enums;

namespace PdfReportsGenerator.Infrastructure.Hubs.Interfaces;

public interface IPdfReportHub
{
    Task ReceivePdfReportTaskStatus(Guid reportTaskId, ReportStatuses reportStatus);
}