namespace PdfReportsGenerator.Application.Infrastructure.Minio;

public interface IPdfReportMinioClient
{
    Task<string> GenerateLinkAsync(string fileName, byte[] fileBytes);

    Task DownloadFileAsync(string fileName, Stream destinationStream);
}