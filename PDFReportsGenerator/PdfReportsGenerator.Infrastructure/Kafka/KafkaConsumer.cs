using Microsoft.Extensions.Options;
using PdfReportsGenerator.Application.Infrastructure.Kafka;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Models.Enums;
using PdfReportsGenerator.Infrastructure.Kafka.Options;

namespace PdfReportsGenerator.Infrastructure.Kafka;

internal sealed class KafkaConsumer(IOptions<KafkaConfigurationOptions> options) : IKafkaConsumer
{
    private readonly KafkaConfigurationOptions _options = options.Value;
    
    public async Task<ReportTaskDto> ConsumeAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Consuming from Kafka");

        return new ReportTaskDto
        {
            Id = default,
            AuthorName = null,
            ReportName = null,
            ReportBody = null,
            ReportS3Link = null,
            Status = ReportStatuses.NotStarted,
            CreatedAt = default,
            UpdatedAt = default
        };
    }
}