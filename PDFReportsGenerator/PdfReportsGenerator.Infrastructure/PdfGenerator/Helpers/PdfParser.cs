using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Helpers;

public class PdfParser : IPdfParser
{
    public ReportObject ParseToObject(ReportTaskDto task)
    {
        throw new NotImplementedException();
    }
}