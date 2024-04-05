using PdfReportsGenerator.Bll.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.BackgroundWorker;

public static class PdfBlocksComposer
{
    public static void ComposeBody(
        this ColumnDescriptor column,
        KafkaRecord kafkaRecord)
    {
        var blocks = kafkaRecord.Report.Blocks;

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
                    column.Item().ComposeImageBlock(imageBlock, kafkaRecord.TaskId.ToString());
                    break;
                case TableBlock tableBlock:
                    column.Item().ComposeTableBlock(tableBlock);
                    break;
            }
        }
    }

    private static void ComposeImageBlock(
        this IContainer container,
        ImageBlock imageBlock,
        string? imageName)
    {
        if (imageBlock.Content is null || imageName is null)
            return;

        using var imageProvider = new ImageProvider(imageBlock.Content, imageName);
        var imagePath = imageProvider.GetImagePath();

        // How to assign image size properly?
        // TODO: Fix it.
        container.Width(10, Unit.Centimetre).Image(imagePath).WithRasterDpi(72);
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
        var n = tableBlock.Content?.Length ?? 0;
        var m = tableBlock.Content?.Max(x => x?.Length) ?? 0;

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
                    var row = tableBlock.Content![i] ?? new string[m];

                    if (i == 0)
                    {
                        foreach (var cell in row)
                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderBlock).Text(cell ?? " ").FontSize(13).Bold();
                            });
                        
                        continue;
                    }

                    foreach (var cell in row)
                    {
                        table.Cell().Element(Block).Text(cell ?? " ").FontSize(13);
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
}