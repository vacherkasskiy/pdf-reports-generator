using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;

public interface IPdfImageProvider
{
    Task<Image> GetImageAsync(string imageUrl);
}