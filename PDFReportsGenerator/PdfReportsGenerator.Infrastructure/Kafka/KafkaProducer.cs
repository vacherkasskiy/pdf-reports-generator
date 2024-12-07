using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Infrastructure.Kafka;

internal sealed class KafkaProducer : IKafkaProducer
{
    public Task ProduceAsync(ReportTaskDto report)
    {
        throw new NotImplementedException();
    }
}