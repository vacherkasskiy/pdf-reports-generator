using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Helpers;

internal class PdfBlocksComposer : IPdfBlocksComposer
{
    public void ComposeBody(
        GridDescriptor grid,
        ReportObject reportObject,
        Dictionary<string, Image> images)
    {
        var blocks = reportObject.Blocks;

        if (blocks is null)
        {
            return;
        }

        foreach (var reportBlock in blocks)
        {
            var width = (int)reportBlock!.Width!;
            var item = GetMarginedContainer(grid.Item(width), reportBlock.Margin);

            switch (reportBlock)
            {
                case TextBlock textBlock:
                    ComposeTextBlock(item, textBlock);
                    break;
                case ImageBlock imageBlock:
                    item.Image(images[imageBlock.Content]);
                    break;
                case TableBlock tableBlock:
                    ComposeTableBlock(item, tableBlock);
                    break;
            }
        }
    }

    #region Private Members

    private static void ComposeTextBlock(
        IContainer container,
        TextBlock textBlock)
    {
        switch (textBlock.Style?.Position)
        {
            case "left":
                container.AlignLeft();
                break;
            case "center":
                container.AlignCenter();
                break;
            case "right":
                container.AlignRight();
                break;
        }

        container
            .Text(textBlock.Content)
            .FontSize((textBlock.Style?.Size ?? 5) * 10);
    }

    private static void ComposeTableBlock(
        IContainer container,
        TableBlock tableBlock)
    {
        var n = tableBlock.Content?.Length ?? 0;
        var m = tableBlock.Content?.Max(x => x?.Length) ?? 0;

        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                for (var i = 0; i < m; ++i)
                    columns.RelativeColumn();
            });

            for (var i = 0; i < n; ++i)
            {
                var row = tableBlock.Content![i] ?? new string[m];

                if (i == 0)
                {
                    foreach (var cell in row)
                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderBlock).Text(cell ?? " ").FontSize(8).Bold();
                        });

                    continue;
                }

                foreach (var cell in row)
                {
                    table.Cell().Element(Block).Text(cell ?? " ").FontSize(8);
                }
            }

            static IContainer Block(IContainer container)
            {
                return container
                    .Border(1)
                    .Background(Colors.White)
                    .Padding(5);
            }

            static IContainer HeaderBlock(IContainer container)
            {
                return container
                    .Border(1)
                    .Background("#D3D3D3")
                    .Padding(5);
            }
        });
    }

    private static IContainer GetMarginedContainer(
        IContainer container,
        Margin? margin)
    {
        if (margin == null)
        {
            return container;
        }

        return container
            .PaddingTop(margin.Top ?? 0)
            .PaddingBottom(margin.Bottom ?? 0)
            .PaddingLeft(margin.Left ?? 0)
            .PaddingRight(margin.Right ?? 0);
    }

    #endregion
}