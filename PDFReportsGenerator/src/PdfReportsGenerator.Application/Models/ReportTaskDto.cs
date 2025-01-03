using PdfReportsGenerator.Application.Models.Enums;

namespace PdfReportsGenerator.Application.Models;

public sealed record ReportTaskDto
{
    public Guid Id { get; init; }

    public required string AuthorName { get; init; }

    public required string ReportName { get; init; }

    public required string ReportBody { get; init; }

    public string? ReportS3Link { get; init; }

    public ReportStatuses Status { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; init; }
}