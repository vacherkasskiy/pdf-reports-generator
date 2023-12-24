using PdfReportsGenerator.Api.Restful.Requests;
using PdfReportsGenerator.Bll.Models;
using Block = PdfReportsGenerator.Api.Restful.Requests.Block;
using BlockBll = PdfReportsGenerator.Bll.Models.Block;
using ImageBlock = PdfReportsGenerator.Api.Restful.Requests.ImageBlock;
using ImageBlockBll = PdfReportsGenerator.Bll.Models.ImageBlock;
using TextBlock = PdfReportsGenerator.Api.Restful.Requests.TextBlock;
using TextBlockBll = PdfReportsGenerator.Bll.Models.TextBlock;
using Style = PdfReportsGenerator.Bll.Models.Style;
using LocationBll = PdfReportsGenerator.Bll.Models.Location;
using TableBlock = PdfReportsGenerator.Api.Restful.Requests.TableBlock;
using TableBlockBll = PdfReportsGenerator.Bll.Models.TableBlock;

namespace PdfReportsGenerator.Api.Restful.Parsers;

public class Parser
{
    public static Report ParseRequestToReport(CreateReportTaskRequest request)
    {
        var report = new Report();
        report.Name = request.Name;
        var blocks = new List<BlockBll?>();

        if (request.Blocks == null) return report;
        foreach (var block in request.Blocks)
        {
            if (block == null)
            {
                blocks.Add(null);
                continue;
            }

            var blockBll = new BlockBll();

            if (block is TextBlock textBlock) 
                CopyTextBlock(out blockBll, textBlock);
            else if (block is ImageBlock imageBlock)
                CopyImageBlock(out blockBll, imageBlock);
            else if (block is TableBlock tableBlock)
                CopyTableBlock(out blockBll, tableBlock);
            CopyBaseFields(ref blockBll, block);

            blocks.Add(blockBll);
        }

        report.Blocks = blocks.ToArray();
        return report;
    }

    private static void CopyBaseFields(ref BlockBll blockBll, Block block)
    {
        blockBll.Type = block.Type;
        blockBll.Location = new LocationBll
        {
            Left = block.Location!.Left,
            Right = block.Location.Right
        };
    }

    private static void CopyTextBlock(out BlockBll blockBll, TextBlock block)
    {
        var textBlock = new TextBlockBll();
        textBlock.Content = block.Content;

        if (block.Style == null) blockBll = textBlock;
        else
        {
            textBlock.Style = new Style
            {
                Position = block.Style.Position,
                Size = block.Style.Size
            };
        }

        blockBll = textBlock;
    }

    private static void CopyImageBlock(out BlockBll blockBll, ImageBlock block)
    {
        blockBll = new ImageBlockBll
        {
            Content = block.Content
        };
    }

    private static void CopyTableBlock(out BlockBll blockBll, TableBlock block)
    {
        blockBll = new TableBlockBll
        {
            Content = block.Content
        };
    }
}