using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PdfReportsGenerator.BackgroundWorker.Configurations;

namespace PdfReportsGenerator.BackgroundWorker;

public class MyMinioClient
{
    private readonly string _contentType = "application/pdf";
    private readonly MinioConfiguration _minioConfiguration;

    public MyMinioClient(MinioConfiguration minioConfiguration)
    {
        _minioConfiguration = minioConfiguration;
    }

    public async Task<string> GetLink(string fileName)
    {
        var minio = new MinioClient()
            .WithEndpoint(_minioConfiguration.Endpoint)
            .WithCredentials(_minioConfiguration.AccessKey, _minioConfiguration.SecretKey)
            .Build();

        var link = await Run(minio, fileName);
        return link;
    }

    private async Task<string> Run(IMinioClient myMinio, string fileName)
    {
        try
        {
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_minioConfiguration.BucketName)
                .WithObject(fileName)
                .WithFileName(fileName)
                .WithContentType(_contentType);

            await myMinio
                .PutObjectAsync(putObjectArgs)
                .ConfigureAwait(false);

            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                .WithBucket(_minioConfiguration.BucketName)
                .WithObject(fileName)
                .WithExpiry((int)TimeSpan.FromDays(1).TotalSeconds);

            var fullUriString = await myMinio
                .PresignedGetObjectAsync(args)
                .ConfigureAwait(false);

            var fullUri = new Uri(fullUriString);
            var trimmedUri = $"{fullUri.Scheme}://{fullUri.Authority}{fullUri.AbsolutePath}";

            return trimmedUri;
        }
        catch (MinioException e)
        {
            Console.WriteLine($"File upload error: {e.Message}");
            throw;
        }
    }
}