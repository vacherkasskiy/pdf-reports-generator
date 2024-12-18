using MassTransit;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services.Interfaces;
using Serilog;

namespace PdfReportsGenerator.Infrastructure.Kafka;

internal sealed class KafkaReportsConsumer(IKafkaMessagesHandler handler) : IConsumer<ReportTaskDto>
{
    public async Task Consume(ConsumeContext<ReportTaskDto> context)
    {
        await handler.HandleAsync(context.Message);

        Log.Logger.Information($"Report creation with id: {context.Message.Id} consumed");
    }
}