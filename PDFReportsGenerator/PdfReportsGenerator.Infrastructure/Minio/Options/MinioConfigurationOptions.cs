namespace PdfReportsGenerator.Infrastructure.Minio.Options;

public class MinioConfigurationOptions
{
    public required string Url { get; init; }
    
    public required string AccessKey { get; init; }
    
    public required string SecretKey { get; init; }
    
    public required string BucketName { get; init; }
    
    public int? UrlExpirySeconds { get; init; }
}