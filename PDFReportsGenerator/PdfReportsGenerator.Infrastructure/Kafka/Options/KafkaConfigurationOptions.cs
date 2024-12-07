namespace PdfReportsGenerator.Infrastructure.Kafka.Options;

public class KafkaConfigurationOptions
{
    public required string KafkaExternalAddress { get; init; }
    public required string SaslUsername { get; init; }
    public required string SaslPassword { get; init; }
    public required string TopicName { get; init; }
}