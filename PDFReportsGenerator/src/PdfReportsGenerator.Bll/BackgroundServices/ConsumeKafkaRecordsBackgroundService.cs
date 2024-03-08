using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PdfReportsGenerator.Bll.Configurations;

namespace PdfReportsGenerator.Bll.BackgroundServices;

public class ConsumeKafkaRecordsBackgroundService : BackgroundService
{
    private readonly KafkaConfiguration _kafkaConfiguration;
    
    public ConsumeKafkaRecordsBackgroundService(IOptions<KafkaConfiguration> kafkaConfiguration)
    {
        _kafkaConfiguration = kafkaConfiguration.Value;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_kafkaConfiguration.ConsumerConfig).Build();
        consumer.Subscribe(_kafkaConfiguration.TopicName);
        
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));
        
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var consumeResult = consumer.Consume(stoppingToken);
            
            if (consumeResult is null)
            {
                continue;
            }
            
            Console.WriteLine(consumeResult.Message.Value);
        }
        
        consumer.Close();
    }
}