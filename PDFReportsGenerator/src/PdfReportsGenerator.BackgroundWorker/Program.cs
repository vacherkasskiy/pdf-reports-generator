using Confluent.Kafka;
using Newtonsoft.Json;
using PdfReportsGenerator.BackgroundWorker.Configurations;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.BackgroundWorker;

class Program
{
    private static readonly KafkaConfiguration KafkaConfiguration = new KafkaConfiguration();
    
    static async Task Main(string[] args)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(KafkaConfiguration.ConsumerConfig).Build();
        var reportsService = new ReportsServiceProvider().GetReportsService();
        var generator = new PdfGenerator();
        var minioClient = new MyMinioClient();
        consumer.Subscribe(KafkaConfiguration.TopicName);
        
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));
        
        while (await timer.WaitForNextTickAsync())
        {
            var consumeResult = consumer.Consume();
            
            if (consumeResult is null)
            {
                continue;
            }

            var result = consumeResult.Message.Value;
            var record = JsonConvert.DeserializeObject<KafkaRecord>(result)!;
            var task = await reportsService.GetReport(record.TaskId.ToString());
            Console.WriteLine($"[{DateTime.Now}] Report with ID: {task.Id} started generating.");
            task.Status = ReportStatus.Processing;
            await reportsService.UpdateReport(task);
            
            var document = generator.Generate(record);
            var link = await minioClient.SaveDocument(document, record.TaskId.ToString());
            Console.WriteLine($"[{DateTime.Now}] Report with ID: {task.Id} generated.");
            task.Status = ReportStatus.Ready;
            task.Link = link;
            await reportsService.UpdateReport(task);
        }
        
        consumer.Close();
    }
}