using Microsoft.Extensions.Options;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Infrastructure.Kafka.Options;

namespace PdfReportsGenerator.Infrastructure.Kafka;

internal sealed class KafkaProducer(IOptions<KafkaConfigurationOptions> options) : IKafkaProducer
{
    private readonly KafkaConfigurationOptions _options = options.Value;

    public async Task ProduceAsync(ReportTaskDto report)
    {
        Console.WriteLine("ProduceAsync");
    }
}