using PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Helpers;

internal sealed class PdfImageProvider(IHttpClientFactory httpClientFactory) : IPdfImageProvider
{
    /// <summary>
    /// Загружает изображение по URL и возвращает его как объект QuestPDF.Infrastructure.Image.
    /// </summary>
    /// <param name="imageUrl">URL изображения.</param>
    /// <returns>Объект QuestPDF.Infrastructure.Image.</returns>
    public async Task<Image> GetImageAsync(string? imageUrl)
    {
        try
        {
            using var client = httpClientFactory.CreateClient();
            await using var imageStream = await client.GetStreamAsync(imageUrl);
            
            return Image.FromStream(imageStream);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка загрузки изображения: {ex.Message}", ex);
        }
    }
}