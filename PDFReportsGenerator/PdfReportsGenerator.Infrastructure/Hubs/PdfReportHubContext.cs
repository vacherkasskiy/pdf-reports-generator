using Microsoft.AspNetCore.SignalR;
using PdfReportsGenerator.Application.Infrastructure.Hubs;
using PdfReportsGenerator.Application.Models.Enums;
using PdfReportsGenerator.Infrastructure.Hubs.Interfaces;

namespace PdfReportsGenerator.Infrastructure.Hubs;

public class PdfReportHubContext(IHubContext<PdfReportHub, IPdfReportHub> hubContext) : IPdfReportHubContext
{
    public Task ReceivePdfReportTaskStatus(Guid reportTaskId, ReportStatuses status)
    {
        return hubContext.Clients.All.ReceivePdfReportTaskStatus(reportTaskId, status);
    }
}