using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Infrastructure.PdfGenerator;

public interface IPdfGenerator
{
    byte[] Generate(ReportObject reportObject);
}