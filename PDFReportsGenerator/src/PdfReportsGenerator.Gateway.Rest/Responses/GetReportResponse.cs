namespace PdfReportsGenerator.Gateway.Rest.Responses;

public record GetReportResponse(
    string Status,
    string? Link);