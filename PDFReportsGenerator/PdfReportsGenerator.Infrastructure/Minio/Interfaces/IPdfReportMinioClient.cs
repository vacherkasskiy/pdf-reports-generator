namespace PdfReportsGenerator.Infrastructure.Minio.Interfaces;

public interface IPdfReportMinioClient
{
    Task<string> GenerateLink(string fileName);
}