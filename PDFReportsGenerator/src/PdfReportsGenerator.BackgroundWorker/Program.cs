using Newtonsoft.Json;
using PdfReportsGenerator.BackgroundWorker.Configurations;
using PdfReportsGenerator.Core.Entities;
using PdfReportsGenerator.Core.Models;
using PDFReportsGenerator.Kafka;
using Serilog;

namespace PdfReportsGenerator.BackgroundWorker;

class Program
{
    private static readonly MinioConfiguration MinioConfiguration = new();

    private static readonly ILogger Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .CreateLogger();

    static async Task Main()
    {
        Logger.Information("Start application.");
        
        var reportsService = new ReportsServiceProvider().GetReportsService();
        var minioClient = new MyMinioClient(MinioConfiguration);

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

        while (await timer.WaitForNextTickAsync())
        {
            var message = KafkaConsumer.Instance.ConsumeMessage();

            if (message is null)
            {
                continue;
            }
            
            var record = JsonConvert.DeserializeObject<KafkaRecord>(message)!;
            ReportTask task;
            
            try
            {
                task = await reportsService.GetReportAsync(record.TaskId.ToString());
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to find report with ID: {record.TaskId}");
                continue;
            }

            try
            {
                Logger.Information($"Report with ID: {task.Id} started generating.");
                task.Status = ReportStatus.Processing;
                task.UpdatedAt = DateTime.UtcNow;
                await reportsService.UpdateReportAsync(task);

                using var generator = new PdfGenerator(record);
                var reportFileName = generator.Generate();
                var link = await minioClient.GetLink(reportFileName);
                Logger.Information($"Report with ID: {task.Id} generated.");
                task.Status = ReportStatus.Ready;
                task.UpdatedAt = DateTime.UtcNow;
                task.Link = link;
                await reportsService.UpdateReportAsync(task);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to generate report with ID: {task.Id}");
                task.Status = ReportStatus.Error;
                await reportsService.UpdateReportAsync(task);
            }
        }

        KafkaConsumer.Instance.Close();
    }
}