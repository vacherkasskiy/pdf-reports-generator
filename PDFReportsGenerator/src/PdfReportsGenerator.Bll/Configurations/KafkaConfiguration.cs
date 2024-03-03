using Confluent.Kafka;

namespace PdfReportsGenerator.Bll.Configurations;

public static class KafkaConfiguration
{
    private const string KafkaExternalAddress = "192.168.49.2:31662";
    private const string SaslUsername = "user1";
    private const string SaslPassword = "EqIaqPl7VW";
    
    public static string TopicName => "cool-topic";

    public static readonly ProducerConfig ProducerConfig = new ()
    {
        BootstrapServers = KafkaExternalAddress,
        SaslPassword = SaslPassword,
        SaslUsername = SaslUsername,
        SaslMechanism = SaslMechanism.Plain,
        SecurityProtocol = SecurityProtocol.SaslPlaintext
    };
    
    public static readonly ConsumerConfig ConsumerConfig = new ()
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