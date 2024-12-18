using PdfReportsGenerator.Application.Infrastructure.PdfGenerator;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator;

internal sealed class PdfGenerator(IPdfBlocksComposer composer) : IPdfGenerator
{
    /// <summary>
    /// Generates PDF report for following ReportObject and returns it as a byte array.
    /// </summary>
    /// <returns> Byte array containing the PDF report. </returns>
    public byte[] Generate(ReportObject reportObject)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Content()
                    .Grid(async grid => // todo get rid of async
                    {
                        grid.VerticalSpacing(10);
                        grid.HorizontalSpacing(10);

                        await composer.ComposeBodyAsync(grid, reportObject);
                    });
            });
        });
        
        return document.GeneratePdf();
    }
}