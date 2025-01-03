namespace PdfReportsGenerator.Application.Exceptions;

public class ReportNotFoundException : Exception
{
    public ReportNotFoundException()
    {
    }

    public ReportNotFoundException(string? message) : base(message)
    {
    }
}