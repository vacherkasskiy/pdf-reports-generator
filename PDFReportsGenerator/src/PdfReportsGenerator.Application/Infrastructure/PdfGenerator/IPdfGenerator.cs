using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Infrastructure.PdfGenerator;

public interface IPdfGenerator
{
    Task<byte[]> GenerateAsync(ReportObject reportObject);
}