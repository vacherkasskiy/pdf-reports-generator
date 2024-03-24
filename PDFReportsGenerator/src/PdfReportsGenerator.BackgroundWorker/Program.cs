using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PdfReportsGenerator.BackgroundWorker.Configurations;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal.Entities;
using QuestPDF.Fluent;
using QuestPDF.Previewer;

namespace PdfReportsGenerator.BackgroundWorker;

class Program
{
    private static readonly KafkaConfiguration KafkaConfiguration = new KafkaConfiguration();
    
    static async Task Main(string[] args)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(KafkaConfiguration.ConsumerConfig).Build();
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
            var document = generator.Generate(record);
            await minioClient.SaveDocument(document, record.TaskId.ToString());
            //var task = await reportsService!.GetReport(record.TaskId.ToString());
            //task.Status = ReportStatus.Processing;
            //await reportsService.UpdateReport(task);
        }
        
        consumer.Close();
    }
}