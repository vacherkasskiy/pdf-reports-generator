using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;

public interface IPdfParser
{
    ReportObject ParseToObject(ReportTaskDto task);
}