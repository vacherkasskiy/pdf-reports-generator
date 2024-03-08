using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PdfReportsGenerator.Bll.Configurations;
using PdfReportsGenerator.Bll.Services.Interfaces;

namespace PdfReportsGenerator.Bll.Services;

public class ReportKafkaProducer : IKafkaProducer
{
    private readonly KafkaConfiguration _kafkaConfiguration;
    
    public ReportKafkaProducer(IOptions<KafkaConfiguration> kafkaConfiguration)
    {
        _kafkaConfiguration = kafkaConfiguration.Value;
    }
    
    public async Task Produce(string message)
    {
        using var producer = new ProducerBuilder<Null, string>(_kafkaConfiguration.ProducerConfig).Build();
        var produceResult = await producer.ProduceAsync
        (
            _kafkaConfiguration.TopicName,
            new Message<Null, string>
            {
                Value = message
            }
        );

        if (produceResult.Status == PersistenceStatus.NotPersisted)
        {
            throw new Exception("Something went wrong");
        }
    }
}