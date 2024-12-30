using PdfReportsGenerator.Application.Infrastructure.PdfGenerator;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Helpers;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator;

internal sealed class PdfGenerator(IPdfImageProvider imageProvider) : IPdfGenerator
{
    /// <summary>
    /// Generates PDF report for following ReportObject and returns it as a byte array.
    /// </summary>
    /// <returns> Byte array containing the PDF report. </returns>
    public async Task<byte[]> GenerateAsync(ReportObject reportObject)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var imagesDictionary = await GetImagesAsync(reportObject);
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Content()
                    .Grid(grid =>
                    {
                        grid.VerticalSpacing(10);
                        grid.HorizontalSpacing(10);

                        grid.ComposeBody(reportObject, imagesDictionary);
                    });
            });
        });
        
        return document.GeneratePdf();
    }

    private async Task<Dictionary<string, Image>> GetImagesAsync(ReportObject reportObject)
    {
        var imagesTasks = reportObject.Blocks?
            .Where(x => x != null && x is ImageBlock imageBlock && imageBlock.Content != null)
            .Select(x => x as ImageBlock)
            .ToDictionary(
                x => x!.Content,
                x => imageProvider.GetImageAsync(x!.Content));
        
        await Task.WhenAll(imagesTasks.Values);
        
        return imagesTasks.ToDictionary(
            x => x.Key,
            x => x.Value.Result);
    }
}