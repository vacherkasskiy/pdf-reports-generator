// TODO CHANGE CONTRACT
// RECEIVE STRING AT ENDPOINTS
// THEN PARSE TO OBJECTS AND VALIDATE AT APPLICATION

// using PdfReportsGenerator.Core.Models;
// using PdfReportsGenerator.Gateway.Grpc.Parsers.Interfaces;
// using BlockProto = Reports.V1.Block;
// using ReportProto = Reports.V1.CreateReportRequest;
// using TextBlockProto = Reports.V1.TextBlock;
// using ImageBlockProto = Reports.V1.ImageBlock;
// using TableBlockProto = Reports.V1.TableBlock;
// using StyleProto = Reports.V1.Style;
// using MarginProto = Reports.V1.Margin;
//
// namespace PdfReportsGenerator.Gateway.Grpc.Parsers;
//
// public class ReportsParser : IParser<ReportProto, ReportBody>
// {
//     public ReportBody Parse(ReportProto reportProto)
//     {
//         var report = new ReportBody
//         {
//             Name = reportProto.Name,
//             Blocks = reportProto.Blocks.Select(ParseBlock).ToArray()
//         };
//
//         return report;
//     }
//
//     private static Margin? ParseMargin(MarginProto? marginProto)
//     {
//         if (marginProto == null)
//             return null;
//         
//         var margin = new Margin
//         {
//             Top = marginProto.Top,
//             Bottom = marginProto.Bottom,
//             Left = marginProto.Left,
//             Right = marginProto.Right
//         };
//
//         return margin;
//     }
//     
//     private static Block? ParseBlock(BlockProto? blockProto)
//     {
//         if (blockProto == null)
//             return null;
//         
//         Block block;
//
//         switch (blockProto.ContentCase)
//         {
//             case BlockProto.ContentOneofCase.TextBlock:
//                 block = ParseTextBlock(blockProto.TextBlock);
//                 block.Type = "text";
//                 break;
//             case BlockProto.ContentOneofCase.ImageBlock:
//                 block = ParseImageBlock(blockProto.ImageBlock);
//                 block.Type = "image";
//                 break;
//             case BlockProto.ContentOneofCase.TableBlock:
//                 block = ParseTableBlock(blockProto.TableBlock);
//                 block.Type = "table";
//                 break;
//             default:
//                 return null;
//         }
//
//         block.Width = blockProto.Width;
//         block.Margin = ParseMargin(blockProto.Margin);
//
//         return block;
//     }
//
//     private static TextBlock ParseTextBlock(TextBlockProto textBlockProto)
//     {
//         return new TextBlock
//         {
//             Content = textBlockProto.Content,
//             Style = ParseStyle(textBlockProto.Style)
//         };
//     }
//
//     private static Style? ParseStyle(StyleProto? styleProto)
//     {
//         if (styleProto == null)
//             return null;
//         
//         return new Style
//         {
//             Position = styleProto.Position
//                 .ToString()
//                 .ToLower(),
//             Size = styleProto.Size
//         };
//     }
//
//     private static ImageBlock ParseImageBlock(ImageBlockProto imageBlockProto)
//     {
//         return new ImageBlock
//         {
//             Content = imageBlockProto.Content
//         };
//     }
//
//     private static TableBlock ParseTableBlock(TableBlockProto tableBlockProto)
//     {
//         return new TableBlock
//         {
//             Content = tableBlockProto.Rows.Select(row => row.Data.ToArray()).ToArray()
//         };
//     }
// }