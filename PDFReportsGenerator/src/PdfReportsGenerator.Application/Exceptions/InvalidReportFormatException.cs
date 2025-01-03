namespace PdfReportsGenerator.Application.Exceptions;

public class InvalidReportFormatException : Exception
{
    public InvalidReportFormatException()
    {
    }

    public InvalidReportFormatException(string? message) : base(message)
    {
    }
}