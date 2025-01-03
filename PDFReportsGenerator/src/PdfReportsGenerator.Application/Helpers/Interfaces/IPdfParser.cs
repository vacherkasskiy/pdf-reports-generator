using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Helpers.Interfaces;

public interface IPdfParser
{
    ReportObject ParseToObject(ReportTaskDto task);
}