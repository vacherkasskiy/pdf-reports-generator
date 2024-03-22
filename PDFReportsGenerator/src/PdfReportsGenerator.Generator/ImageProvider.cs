using System.Net;

namespace PdfReportsGenerator.Generator;

public class ImageProvider : IDisposable
{
    private readonly string _imageUrl;
    private readonly string _imagePath;
    
    public ImageProvider(string imageUrl, string imageName)
    {
        _imageUrl = imageUrl;
        _imagePath = $"{imageName}.jpg";
    }
    
    public string GetImagePath()
    {
        using WebClient webClient = new WebClient();
        try
        {
            webClient.DownloadFile(_imageUrl, _imagePath);
            return _imagePath;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public void Dispose()
    {
        if (File.Exists(_imagePath))
        {
            File.Delete(_imagePath);
        }
    }
}