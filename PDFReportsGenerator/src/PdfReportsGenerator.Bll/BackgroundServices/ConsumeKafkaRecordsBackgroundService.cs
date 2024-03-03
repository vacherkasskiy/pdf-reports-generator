using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfReportsGenerator.Bll.Configurations;

namespace PdfReportsGenerator.Bll.BackgroundServices;

public class ConsumeKafkaRecordsBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(KafkaConfiguration.ConsumerConfig).Build();
        consumer.Subscribe(KafkaConfiguration.TopicName);

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var consumeResult = consumer.Consume(stoppingToken);
            
            if (consumeResult is null)
            {
                continue;
            }
            
            // Работает!
            Console.WriteLine(consumeResult.Message.Value);
        }
        
        consumer.Close();
    }
}