using PdfReportsGenerator.Application.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;

public interface IPdfBlocksComposer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="reportObject"></param>
    /// <param name="images"> Dictionary of images, in which key is URL and value is QuestPDF Image object. </param>
    void ComposeBody(
        GridDescriptor grid,
        ReportObject reportObject,
        Dictionary<string, Image> images);
}