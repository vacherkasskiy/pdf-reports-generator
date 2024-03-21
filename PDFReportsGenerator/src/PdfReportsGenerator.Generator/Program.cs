using PdfReportsGenerator.Bll.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

// TODO: Change the name?
namespace PdfReportsGenerator.Generator;

/// <summary>
/// TODO: Add images composing.
/// TODO: Move some logic to a different class?.
/// </summary>
static class Program
{
    private static void ComposeBody(
        this ColumnDescriptor column,
        Block?[]? blocks)
    {
        if (blocks is null) 
            return;
        
        foreach (var reportBlock in blocks)
        {
            switch (reportBlock)
            {
                case TextBlock textBlock:
                    column.Item().ComposeTextBlock(textBlock);
                    break;
                case ImageBlock imageBlock:
                    throw new NotImplementedException();
                case TableBlock tableBlock:
                    column.Item().ComposeTableBlock(tableBlock);
                    break;
            }
        }
    }
    
    private static void ComposeTextBlock(
        this IContainer container,
        TextBlock textBlock)
    {
        GetAlignedContainer(container, textBlock.Style!.Position)
            .Text(textBlock.Content)
            .FontSize(textBlock.Style!.Size * 10);
    }

    private static void ComposeTableBlock(
        this IContainer container,
        TableBlock tableBlock)
    {
        var n = tableBlock.Content.Length;
        var m = tableBlock.Content.Max(x => x?.Length);
        
        container.Border(1)
            .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    for (var i = 0; i < m; ++i)
                        columns.RelativeColumn();
                });

                for (var i = 0; i < n; ++i)
                {
                    var row = tableBlock.Content[i] ?? new string[m ?? 0];

                    // TODO: Add table headers?
                    foreach (var cell in row)
                        table.Cell().Element(Block).Text(cell ?? " ").FontSize(13);
                }

                static IContainer Block(IContainer container)
                {
                    return container
                        .Border(1)
                        .Background(Colors.White)
                        .Padding(5);
                }
            });
    }

    private static IContainer GetAlignedContainer(
        IContainer container,
        string? position)
    {
        switch (position)
        {
            case "left":
                return container.AlignLeft();
            case "center":
                return container.AlignCenter();
            case "right":
                return container.AlignRight();
            default:
                return container;
        }
    }

    public static void Main()
    {
        // TODO: Delete this.
        // WILL NOT PRESENT IN PRODUCTION VERSION.
        var report = ReportFaker.GetReport();
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);
                        x.ComposeBody(report.Blocks);
                    });

                // page.Footer()
                //     .AlignCenter()
                //     .Text(x =>
                //     {
                //         x.Span("Page ");
                //         x.CurrentPageNumber();
                //     });
            });
        });

        // TODO: Delete this.
        document.ShowInPreviewer();
    }
}