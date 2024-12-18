namespace PdfReportsGenerator.Application.Infrastructure.Minio;

public interface IPdfReportMinioClient
{
    Task<string> GenerateLink(string fileName);
}