using PdfReportsGenerator.Application.Models;
using QuestPDF.Fluent;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;

public interface IPdfBlocksComposer
{
    Task ComposeBodyAsync(GridDescriptor grid, ReportObject reportObject);
}