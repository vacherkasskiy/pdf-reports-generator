using Confluent.Kafka;

namespace PDFReportsGenerator.Kafka;

public class KafkaProducer
{
    private readonly KafkaConfiguration _kafkaConfiguration = new ();
    
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