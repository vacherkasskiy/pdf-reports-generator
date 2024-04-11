using Confluent.Kafka;

namespace PDFReportsGenerator.Kafka;

internal class KafkaConfiguration
{
    private const string KafkaExternalAddress = "192.168.49.2:31939";
    private const string SaslUsername = "user1";
    private const string SaslPassword = "YmScfOHfF5";
    
    public string TopicName => "cool-topic";
    
    public ProducerConfig ProducerConfig => new ()
    {
        BootstrapServers = KafkaExternalAddress,
        SaslPassword = SaslPassword,
        SaslUsername = SaslUsername,
        SaslMechanism = SaslMechanism.Plain,
        SecurityProtocol = SecurityProtocol.SaslPlaintext
    };
    
    public ConsumerConfig ConsumerConfig => new ()
    {
        BootstrapServers = KafkaExternalAddress,
        GroupId = "group-id",
        SaslPassword = SaslPassword,
        SaslUsername = SaslUsername,
        SaslMechanism = SaslMechanism.Plain,
        SecurityProtocol = SecurityProtocol.SaslPlaintext,
        AutoOffsetReset = AutoOffsetReset.Earliest
    };
}