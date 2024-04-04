using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using QuestPDF.Fluent;

namespace PdfReportsGenerator.BackgroundWorker;

public class MyMinioClient
{
    public async Task<string> SaveDocument(Document document, string fileName)
    {
        var link = "link";
        document.GeneratePdf(fileName);
        Method(fileName);
        
        return link;
    }
    
    public static void Method(string fileName)
    {
        var endpoint = "192.168.49.2:30003";
        var accessKey = "minio";
        var secretKey = "miniosecret";

        try
        {
            var minio = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .Build();
            Run(minio, fileName).Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }

    // File uploader task.
    private static async Task Run(IMinioClient myMinio, string filePath)
    {
        var bucketName = "reports";
        var objectName = filePath;
        var contentType = "application/pdf";

        try
        {
            // Make a bucket on the server, if not already present.
            var beArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            bool found = await myMinio.BucketExistsAsync(beArgs).ConfigureAwait(false);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await myMinio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
            }

            // Upload a file to bucket.
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithFileName(filePath)
                .WithContentType(contentType);
            await myMinio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            Console.WriteLine("Successfully uploaded " + objectName);
        }
        catch (MinioException e)
        {
            Console.WriteLine("File Upload Error: {0}", e.Message);
        }
    }
}