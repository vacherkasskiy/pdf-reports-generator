using Confluent.Kafka;

namespace PdfReportsGenerator.Bll.Services;

public class ReportKafka
{
    private const string KafkaExternalAddress = "192.168.49.2:31662";
    private const string SaslUsername = "user1";
    private const string SaslPassword = "EqIaqPl7VW";
    private const string TopicName = "cool-topic";

    private readonly ProducerConfig _producerConfig = new ProducerConfig
    {
        BootstrapServers = KafkaExternalAddress,
        SaslPassword = SaslPassword,
        SaslUsername = SaslUsername,
        SaslMechanism = SaslMechanism.Plain,
        SecurityProtocol = SecurityProtocol.SaslPlaintext
    };
    
    private readonly ConsumerConfig _consumerConfig = new ConsumerConfig
    {
        BootstrapServers = KafkaExternalAddress,
        GroupId = "group-id",
        SaslPassword = SaslPassword,
        SaslUsername = SaslUsername,
        SaslMechanism = SaslMechanism.Plain,
        SecurityProtocol = SecurityProtocol.SaslPlaintext,
        AutoOffsetReset = AutoOffsetReset.Earliest
    };
    
    public async Task Produce(string message)
    {
        using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
        var produceResult = await producer.ProduceAsync
        (
            TopicName,
            new Message<Null, string>
            {
                Value = message
            }
        );

        if (produceResult.Status == PersistenceStatus.NotPersisted)
            throw new Exception("Something went wrong");
    }
    
    public async Task<string> Consume()
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        consumer.Subscribe(TopicName);
        
        var consumeResult = consumer.Consume();

        if (consumeResult == null)
            throw new Exception("Something went wrong");

        return consumeResult.Message.Value;
    }
}