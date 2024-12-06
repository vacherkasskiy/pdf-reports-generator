using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Infrastructure.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(ReportTaskDto report);
}