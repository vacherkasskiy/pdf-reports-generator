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
                    Content = "Size 1",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 1)
                },
                new TextBlock
                {
                    Content = "Size 2",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 2)
                },
                new TextBlock
                {
                    Content = "Size 3",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 3)
                },
                new TextBlock
                {
                    Content = "Size 4",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 4)
                },
                new TextBlock
                {
                    Content = "Size 5",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 5)
                },
                new TextBlock
                {
                    Content = "Size 6",
                    Location = DefaultLocation,
                    Style = GetStyle(size: 6)
                },
                new TextBlock
                {
                    Content = "Left alignment",
                    Location = DefaultLocation,
                    Style = GetStyle(position: "left")
                },
                new TextBlock
                {
                    Content = "Center alignment",
                    Location = DefaultLocation,
                    Style = GetStyle(position: "center")
                },
                new TextBlock
                {
                    Content = "Right alignment",
                    Location = DefaultLocation,
                    Style = GetStyle(position: "right")
                },
                new ImageBlock
                {
                    Content = "https://i.pinimg.com/236x/b3/38/d2/b338d2779e7af3955b411807b12575ee.jpg",
                    Location = DefaultLocation
                },
                new ImageBlock
                {
                    Content = "https://i.pinimg.com/236x/61/1f/50/611f5033ea1e608440652d8201aa2df3.jpg",
                    Location = DefaultLocation
                },
                new ImageBlock
                {
                    Content = "https://i.pinimg.com/236x/69/51/ce/6951cef18f825a88a1b1f563a5738591.jpg",
                    Location = DefaultLocation
                },
                new TableBlock
                {
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