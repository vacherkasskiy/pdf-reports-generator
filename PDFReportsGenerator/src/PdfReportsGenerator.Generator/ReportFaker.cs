using PdfReportsGenerator.Bll.Models;

namespace PdfReportsGenerator.Generator;

/// <summary>
/// TODO: Delete this.
/// WILL NOT PRESENT IN PRODUCTION VERSION.
/// </summary>
public static class ReportFaker
{
    private static readonly Location DefaultLocation = new Location
    {
        Left = 1,
        Right = 12
    };

    private static Style GetStyle(
        string position = "left",
        int size = 3)
    {
        return new Style
        {
            Position = position,
            Size = size
        };
    }

    public static Report GetReport()
    {
        var report = new Report()
        {
            Name = "NEW PDF REPORT #1",
            Blocks = new Block?[]
            {
                new TextBlock
                {
                    Type = "text",
                    Content = "Size 1",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 1)
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Size 2",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 2)
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Size 3",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 3)
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Size 4",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 4)
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Size 5",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 5)
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Size 6",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 6)
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Left alignment",
                    Location = DefaultLocation,
                    Style = GetStyle(position: "left")
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Center alignment",
                    Location = DefaultLocation,
                    Style = GetStyle(position: "center")
                },
                new TextBlock
                {
                    Type = "text",
                    Content = "Right alignment",
                    Location = DefaultLocation,
                    Style = GetStyle(position: "right")
                },
                new TableBlock
                {
                    Type = "table",
                    Content = new[]
                    {
                        new[] { "A", "B", "C", null, "" },
                        new[] { "A", null, "C", null, "" },
                        null,
                        new[] { "A", "B", "C", "D", "LFKBNDFLK" },
                    }
                }
            }
        };

        return report;
    }
}