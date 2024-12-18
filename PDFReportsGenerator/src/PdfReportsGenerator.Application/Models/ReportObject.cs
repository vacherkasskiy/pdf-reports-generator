namespace PdfReportsGenerator.Application.Models;

public class ReportObject
{
    public Guid TaskId { get; set; }

    public ReportBody ReportBody { get; set; }
}