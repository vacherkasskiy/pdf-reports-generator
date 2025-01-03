using PdfReportsGenerator.Application.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.Infrastructure.PdfGenerator.Helpers;

internal static class PdfBlocksComposer
{
    public static void ComposeBody(
        this GridDescriptor grid,
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

            switch (reportBlock)
            {
                case TextBlock textBlock:
                    grid.Item(width)
                        .GetMarginedContainer(textBlock.Margin)
                        .ComposeTextBlock(textBlock);
                    break;
                case ImageBlock imageBlock:
                    grid.Item(width)
                        .GetMarginedContainer(imageBlock.Margin)
                        .Image(images[imageBlock.Content]);
                    break;
                case TableBlock tableBlock:
                    grid.Item(width)
                        .GetMarginedContainer(tableBlock.Margin)
                        .ComposeTableBlock(tableBlock);
                    break;
            }
        }
    }

    private static void ComposeTextBlock(
        this IContainer container,
        TextBlock textBlock)
    {
        container
            .GetAlignedContainer(textBlock.Style!.Position)
            .Text(textBlock.Content)
            .FontSize(textBlock.Style!.Size * 10);
    }

    private static void ComposeTableBlock(
        this IContainer container,
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

    private static IContainer GetAlignedContainer(
        this IContainer container,
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

    private static IContainer GetMarginedContainer(
        this IContainer container,
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
}