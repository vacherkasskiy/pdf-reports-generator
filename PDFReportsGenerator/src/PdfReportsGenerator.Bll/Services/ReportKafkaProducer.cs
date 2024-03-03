using Confluent.Kafka;
using PdfReportsGenerator.Bll.Configurations;
using PdfReportsGenerator.Bll.Services.Interfaces;

namespace PdfReportsGenerator.Bll.Services;

public class ReportKafkaProducer : IKafkaProducer
{
    public async Task Produce(string message)
    {
        using var producer = new ProducerBuilder<Null, string>(KafkaConfiguration.ProducerConfig).Build();
        var produceResult = await producer.ProduceAsync
        (
            KafkaConfiguration.TopicName,
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