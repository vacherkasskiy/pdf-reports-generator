using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PdfReportsGenerator.Bll.Configurations;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.Bll.BackgroundServices;

public class ConsumeKafkaRecordsBackgroundService : BackgroundService
{
    private readonly KafkaConfiguration _kafkaConfiguration;
    private readonly IServiceProvider _serviceProvider;
    
    public ConsumeKafkaRecordsBackgroundService(
        IOptions<KafkaConfiguration> kafkaConfiguration,
        IServiceProvider serviceProvider)
    {
        _kafkaConfiguration = kafkaConfiguration.Value;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_kafkaConfiguration.ConsumerConfig).Build();
        consumer.Subscribe(_kafkaConfiguration.TopicName);
        
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));
        
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceProvider.CreateScope();
            var reportsService = scope.ServiceProvider.GetService<IReportsService>();
            
            var consumeResult = consumer.Consume(stoppingToken);
            
            if (consumeResult is null)
            {
                continue;
            }

            var result = consumeResult.Message.Value;
            var record = JsonSerializer.Deserialize<KafkaRecord>(result)!;
            var task = await reportsService!.GetReport(record.TaskId.ToString());
            task.Status = ReportStatus.Processing;
            await reportsService.UpdateReport(task);
        }
        
        consumer.Close();
    }
}