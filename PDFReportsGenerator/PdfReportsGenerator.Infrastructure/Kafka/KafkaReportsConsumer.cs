using MassTransit;
using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Infrastructure.Kafka;

internal sealed class KafkaReportsConsumer : IConsumer<ReportTaskDto>
{
    public async Task Consume(ConsumeContext<ReportTaskDto> context)
    {
        Console.WriteLine($"Message consumed:\n{context.Message}");
    }
}