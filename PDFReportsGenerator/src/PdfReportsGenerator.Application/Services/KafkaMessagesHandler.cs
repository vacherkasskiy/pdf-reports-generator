using FluentValidation;
using PdfReportsGenerator.Application.Helpers.Interfaces;
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
    IPdfParser parser,
    IValidator<ReportObject> validator,
    IPdfGenerator pdfGenerator) : IKafkaMessagesHandler
{
    public async Task HandleAsync(ReportTaskDto reportTask)
    {
        try
        {
            var reportObject = parser.ParseToObject(reportTask);
            var validationResult = await validator.ValidateAsync(reportObject);

            if (!validationResult.IsValid)
            {
                var concatenatedErrors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                
                Log.Logger.Error($"Invalid template of the report with ID {reportTask.Id}: {concatenatedErrors}");
                await UpdateStatusAsync(ReportStatuses.InvalidTemplate, reportTask.Id);
                return;
            }
            
            await UpdateStatusAsync(ReportStatuses.Processing, reportTask.Id);
            
            var pdfDocumentBytes = await pdfGenerator.GenerateAsync(reportObject);
            var link = await minioClient.GenerateLinkAsync(reportTask.Id.ToString(), pdfDocumentBytes); // todo потом, при попытке скачать - идти в бд и брать оттуда имя отчета.
            
            await UpdateStatusAsync(ReportStatuses.Ready, reportTask.Id, link);
        }
        catch (Exception e)
        {
            Log.Error(e, $"Error while processing report task with ID {reportTask.Id}: {e.Message}");
            await UpdateStatusAsync(ReportStatuses.Error, reportTask.Id);
        }
    }

    private async Task UpdateStatusAsync(ReportStatuses status, Guid reportTaskId, string? link = null)
    {
        await reportTaskService.SetStatusToReportAsync(reportTaskId, status, link);
        await hubContext.ReceivePdfReportTaskStatus(reportTaskId, status);
        await dbContext.SaveChangesAsync();
    }
}