using PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;
using PdfReportsGenerator.Bll.Models;
using BlockProto = Reports.V1.Block;
using ReportProto = Reports.V1.CreateReportRequest;
using TextBlockProto = Reports.V1.TextBlock;
using ImageBlockProto = Reports.V1.ImageBlock;
using TableBlockProto = Reports.V1.TableBlock;
using LocationProto = Reports.V1.Location;
using StyleProto = Reports.V1.Style;

namespace PdfReportsGenerator.Api.Grpc.Parsers;

public class ReportsParser : IReportsParser
{
    public Report ParseReport(ReportProto reportProto)
    {
        var report = new Report
        {
            Name = reportProto.Name,
            Blocks = reportProto.Blocks.Select(ParseBlock).ToArray()
        };

        return report;
    }

    private static Block? ParseBlock(BlockProto blockProto)
    {
        Block block;

        switch (blockProto.ContentCase)
        {
            case BlockProto.ContentOneofCase.TextBlock:
                block = ParseTextBlock(blockProto.TextBlock);
                block.Type = "text";
                break;
            case BlockProto.ContentOneofCase.ImageBlock:
                block = ParseImageBlock(blockProto.ImageBlock);
                block.Type = "image";
                break;
            case BlockProto.ContentOneofCase.TableBlock:
                block = ParseTableBlock(blockProto.TableBlock);
                block.Type = "table";
                break;
            default:
                return null;
        }
        
        block.Location = ParseLocation(blockProto.Location);

        return block;
    }

    private static Location ParseLocation(LocationProto locationProto)
    {
        return new Location
        {
            Left = locationProto.Left,
            Right = locationProto.Right
        };
    }

    private static TextBlock ParseTextBlock(TextBlockProto textBlockProto)
    {
        return new TextBlock
        {
            Content = textBlockProto.Content,
            Style = ParseStyle(textBlockProto.Style)
        };
    }

    private static Style ParseStyle(StyleProto styleProto)
    {
        return new Style
        {
            Position = styleProto.Position
                .ToString()
                .ToLower(),
            Size = styleProto.Size
        };
    }

    private static ImageBlock ParseImageBlock(ImageBlockProto imageBlockProto)
    {
        return new ImageBlock
        {
            Content = imageBlockProto.Content
        };
    }

    private static TableBlock ParseTableBlock(TableBlockProto tableBlockProto)
    {
        return new TableBlock
        {
            Content = tableBlockProto.Rows.Select(row => row.Data.ToArray()).ToArray()
        };
    }
}