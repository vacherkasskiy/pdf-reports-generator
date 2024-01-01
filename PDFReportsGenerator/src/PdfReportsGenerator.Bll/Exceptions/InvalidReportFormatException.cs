namespace PdfReportsGenerator.Bll.Exceptions;

public class InvalidReportFormatException : Exception
{
    public InvalidReportFormatException()
    {
    }

    public InvalidReportFormatException(string? message) : base(message)
    {
    }
}