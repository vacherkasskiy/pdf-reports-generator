namespace PdfReportsGenerator.Gateway.Rest.Requests;

public sealed record CreateReportRequest
{
    public required string AuthorName { get; init; }
    
    public required string ReportName { get; init; }
    
    public required string ReportBody { get; init; }
}