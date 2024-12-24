using PdfReportsGenerator.Application.Infrastructure.Hubs;
using PdfReportsGenerator.Application.Infrastructure.Minio;
using PdfReportsGenerator.Application.Infrastructure.PdfGenerator;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Models.Enums;
using PdfReportsGenerator.Application.Services.Interfaces;
using Serilog;

namespace PdfReportsGenerator.Application.Services;

public class KafkaMessagesHandler(
    IReportTaskService reportTaskService,
    IPdfGeneratorDbContext dbContext,
    IPdfReportHubContext hubContext,
    IPdfReportMinioClient minioClient,
    IPdfGenerator pdfGenerator) : IKafkaMessagesHandler
{
    public async Task HandleAsync(ReportTaskDto reportTask)
    {
        try
        {
            await UpdateStatusAsync(ReportStatuses.Processing, reportTask.Id);
            
            var pdfDocumentBytes = await pdfGenerator.GenerateAsync(reportTask);
            var link = await minioClient.GenerateLinkAsync(reportTask.ReportName, pdfDocumentBytes);
            
            await UpdateStatusAsync(ReportStatuses.Ready, reportTask.Id, link);
        }
        catch (Exception e)
        {
            await UpdateStatusAsync(ReportStatuses.Error, reportTask.Id);
            Log.Error(e, $"Error while processing report task: {e.Message}");
        }
    }

    private async Task UpdateStatusAsync(ReportStatuses status, Guid reportTaskId, string? link = null)
    {
        await reportTaskService.SetStatusToReportAsync(reportTaskId, status, link);
        await hubContext.ReceivePdfReportTaskStatus(reportTaskId, status);
        await dbContext.SaveChangesAsync();
    }
}