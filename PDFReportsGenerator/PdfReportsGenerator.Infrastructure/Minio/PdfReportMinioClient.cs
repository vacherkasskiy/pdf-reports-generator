using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PdfReportsGenerator.Application.Infrastructure.Minio;
using PdfReportsGenerator.Infrastructure.Minio.Options;
using Serilog;

namespace PdfReportsGenerator.Infrastructure.Minio;

internal class PdfReportMinioClient : IPdfReportMinioClient
{
    private readonly IMinioClient _minioClient;
    private readonly MinioConfigurationOptions _minioConfiguration;
    private readonly int _urlExpirySeconds;
    private readonly Task _initializationTask;
    private const string ContentType = "application/pdf";

    public PdfReportMinioClient(IOptions<MinioConfigurationOptions> minioConfiguration)
    {
        _minioConfiguration = minioConfiguration.Value;
        _urlExpirySeconds = _minioConfiguration.UrlExpirySeconds ?? (int)TimeSpan.FromDays(1).TotalSeconds;

        _minioClient = new MinioClient()
            .WithEndpoint(_minioConfiguration.Url)
            .WithCredentials(_minioConfiguration.AccessKey, _minioConfiguration.SecretKey)
            .Build();

        _initializationTask = Initialize();
    }

    public async Task<string> GenerateLink(string fileName)
    {
        try
        {
            await _initializationTask.ConfigureAwait(false);

            ValidateOrThrow(fileName);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_minioConfiguration.BucketName)
                .WithObject(fileName)
                .WithFileName(fileName)
                .WithContentType(ContentType);

            await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

            Log.Logger.Information($"MinIO successfully received file {fileName}");

            var args = new PresignedGetObjectArgs()
                .WithBucket(_minioConfiguration.BucketName)
                .WithObject(fileName)
                .WithExpiry(_urlExpirySeconds);

            var fullUriString = await _minioClient.PresignedGetObjectAsync(args).ConfigureAwait(false);

            return TrimUri(fullUriString);
        }
        catch (MinioException e)
        {
            Log.Logger.Error(e, $"File upload error for file {fileName}");
            throw;
        }
    }

    #region Private Members

    private async Task Initialize()
    {
        try
        {
            var beArgs = new BucketExistsArgs().WithBucket(_minioConfiguration.BucketName);
            var found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);

            if (!found)
            {
                var mbArgs = new MakeBucketArgs().WithBucket(_minioConfiguration.BucketName);
                await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
            }
        }
        catch (MinioException ex)
        {
            Log.Logger.Error(ex, $"Failed to initialize Minio client for bucket {_minioConfiguration.BucketName}");
            throw;
        }
    }

    private static void ValidateOrThrow(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
        }

        if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        {
            throw new ArgumentException("File name contains invalid characters.", nameof(fileName));
        }
    }

    private static string TrimUri(string fullUriString)
    {
        var fullUri = new Uri(fullUriString);

        return $"{fullUri.Scheme}://{fullUri.Authority}{fullUri.AbsolutePath}";
    }

    #endregion
}