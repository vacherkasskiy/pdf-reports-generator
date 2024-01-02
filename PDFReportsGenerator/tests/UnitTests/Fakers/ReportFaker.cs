using AutoBogus;
using Bogus;
using PdfReportsGenerator.Bll.Models;

namespace UnitTests.Fakers;

public static class ReportFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Style> StyleFaker = new AutoFaker<Style>()
        .RuleFor(x => x.Position, f => f.PickRandom("left", "center", "right"))
        .RuleFor(x => x.Size, f => f.Random.Number(1, 6));

    private static readonly Faker<Location> LocationFaker = new AutoFaker<Location>()
        .RuleFor(x => x.Left, f => f.Random.Number(1, 12))
        .RuleFor(x => x.Right, f => f.Random.Number(1, 12));

    private static readonly Faker<TextBlock> TextBlockFaker = new AutoFaker<TextBlock>()
        .RuleFor(x => x.Content, f => f.Lorem.Sentence())
        .RuleFor(x => x.Style, _ => StyleFaker.Generate())
        .RuleFor(x => x.Type, "text")
        .RuleFor(x => x.Location, f => LocationFaker.Generate());

    private static readonly Faker<ImageBlock> ImageBlockFaker = new AutoFaker<ImageBlock>()
        .RuleFor(x => x.Content, f => f.Image.LoremPixelUrl())
        .RuleFor(x => x.Type, "image")
        .RuleFor(x => x.Location, f => LocationFaker.Generate());

    private static readonly Faker<TableBlock> TableBlockFaker = new AutoFaker<TableBlock>()
        .RuleFor(x => x.Content,
            f => f.Make(f.Random.Number(1, 5), _ => f.Lorem.Words(f.Random.Number(1, 5))).ToArray())
        .RuleFor(x => x.Type, "table")
        .RuleFor(x => x.Location, _ => LocationFaker.Generate());

    private static readonly Faker<Report> Faker = new AutoFaker<Report>()
        .RuleFor(x => x.Name, f => f.Lorem.Word());

    private static IEnumerable<Report> GenerateWithBlocks(int count, Block[]? blocks)
    {
        lock (Lock)
        {
            var reports = new List<Report>();
            for (int i = 0; i < count; i++)
            {
                var report = Faker.Generate();
                report.Blocks = blocks;
                reports.Add(Faker.Generate());
            }

            return reports;
        }
    }

    public static IEnumerable<Report> Generate(int count = 1) =>
        GenerateWithBlocks(count,
            new Block[] {TextBlockFaker.Generate(), ImageBlockFaker.Generate(), TableBlockFaker.Generate()});

    public static IEnumerable<Report> GenerateWithTextBlock(int count = 1) =>
        GenerateWithBlocks(count, new Block[] {TextBlockFaker.Generate()});

    public static IEnumerable<Report> GenerateWithImageBlock(int count = 1) =>
        GenerateWithBlocks(count, new Block[] {ImageBlockFaker.Generate()});

    public static IEnumerable<Report> GenerateWithTableBlock(int count = 1) =>
        GenerateWithBlocks(count, new Block[] {TableBlockFaker.Generate()});
}