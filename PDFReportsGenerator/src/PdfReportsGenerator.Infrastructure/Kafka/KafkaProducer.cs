using MassTransit;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Infrastructure.Kafka;

internal sealed class KafkaProducer(ITopicProducer<ReportTaskDto> producer) : IKafkaProducer
{
    public async Task ProduceAsync(ReportTaskDto report)
    {
        await producer.Produce(report);
    }
}