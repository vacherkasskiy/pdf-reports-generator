using Minio.DataModel.Args;

namespace PdfReportsGenerator.BackgroundWorker.Configurations;

public class MinioConfiguration
{
    public string Endpoint => "192.168.49.2:30003";
    public string AccessKey => "minio";
    public string SecretKey => "miniosecret";
    public string BucketName => "reports";

    public PutObjectArgs GetPutObjectArgs(
        string objectName,
        string fileName,
        string contentType)
    {
        return new PutObjectArgs()
            .WithBucket(BucketName)
            .WithObject(objectName)
            .WithFileName(fileName)
            .WithContentType(contentType);
    }
}