using Confluent.Kafka;

namespace PDFReportsGenerator.Kafka;

public class KafkaProducer
{
    private readonly KafkaConfiguration _kafkaConfiguration = new ();
    
    public async Task Produce(string key, string message)
    {
        using var producer = new ProducerBuilder<string, string>(_kafkaConfiguration.ProducerConfig).Build();
        var produceResult = await producer.ProduceAsync
        (
            _kafkaConfiguration.TopicName,
            new Message<string, string>
            {
                Key = key,
                Value = message
            }
        );

        if (produceResult.Status == PersistenceStatus.NotPersisted)
        {
            throw new Exception("Something went wrong");
        }
    }
}