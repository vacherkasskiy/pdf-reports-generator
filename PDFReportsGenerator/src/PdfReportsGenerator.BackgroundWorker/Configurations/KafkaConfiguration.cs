using Confluent.Kafka;

namespace PdfReportsGenerator.BackgroundWorker.Configurations;

public class KafkaConfiguration
{
    public string KafkaExternalAddress => "192.168.49.2:31662";
    public string SaslUsername => "user1";
    public string SaslPassword => "EqIaqPl7VW";
    
    public string TopicName => "cool-topic";
    
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