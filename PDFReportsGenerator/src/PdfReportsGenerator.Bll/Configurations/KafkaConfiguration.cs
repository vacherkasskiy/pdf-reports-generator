using Confluent.Kafka;

namespace PdfReportsGenerator.Bll.Configurations;

public class KafkaConfiguration
{
    public static string SectionName => "KafkaConfiguration";
    public string KafkaExternalAddress { get; init; }
    public string SaslUsername { get; init; }
    public string SaslPassword { get; init; }
    
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