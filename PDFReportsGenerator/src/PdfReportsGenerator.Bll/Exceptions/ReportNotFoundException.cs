namespace PdfReportsGenerator.Bll.Exceptions;

public class ReportNotFoundException : Exception
{
    public ReportNotFoundException()
    {
    }

    public ReportNotFoundException(string? message) : base(message)
    {
    }
}