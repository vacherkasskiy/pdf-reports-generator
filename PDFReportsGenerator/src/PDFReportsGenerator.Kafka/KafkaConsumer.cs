using Confluent.Kafka;

namespace PDFReportsGenerator.Kafka;

public class KafkaConsumer2
{
    private static KafkaConsumer2? _instance;
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly KafkaConfiguration _kafkaConfiguration = new();

    private KafkaConsumer2()
    {
        _consumer = new ConsumerBuilder<Ignore, string>(_kafkaConfiguration.ConsumerConfig).Build();
        _consumer.Subscribe(_kafkaConfiguration.TopicName);
    }

    public static KafkaConsumer2 Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new KafkaConsumer2();
            }
            return _instance;
        }
    }

    public string? ConsumeMessage()
    {
        var consumeResult = _consumer.Consume();

        if (consumeResult is null)
        {
            return null;
        }
        
        return consumeResult.Message.Value;
    }
    
    public void Close()
    {
        _consumer.Close();
        _consumer.Dispose();
    }
}
