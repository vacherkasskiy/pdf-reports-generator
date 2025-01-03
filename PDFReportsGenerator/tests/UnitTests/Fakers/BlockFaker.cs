using AutoBogus;
using Bogus;
using PdfReportsGenerator.Application.Models;

namespace UnitTests.Fakers;

public class BlockFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Margin> MarginFaker = new AutoFaker<Margin>()
        .RuleFor(x => x.Top, f => f.Random.Number(-100, 100))
        .RuleFor(x => x.Bottom, f => f.Random.Number(-100, 100))
        .RuleFor(x => x.Right, f => f.Random.Number(-100, 100))
        .RuleFor(x => x.Left, f => f.Random.Number(-100, 100));

    private static readonly Faker<Block> AbstractFaker = new AutoFaker<Block>()
        .RuleFor(x => x.Type, f => f.PickRandom("text", "image", "table"))
        .RuleFor(x => x.Width, f => f.Random.Number(1, 12))
        .RuleFor(x => x.Margin, _ => MarginFaker.Generate());

    private static readonly Faker<Style> StyleFaker = new AutoFaker<Style>()
        .RuleFor(x => x.Position, f => f.PickRandom("left", "center", "right"))
        .RuleFor(x => x.Size, f => f.Random.Number(1, 6));

    private static readonly Faker<TextBlock> TextBlockFaker = new AutoFaker<TextBlock>()
        .RuleFor(x => x.Content, f => f.Lorem.Sentence())
        .RuleFor(x => x.Style, _ => StyleFaker.Generate())
        .RuleFor(x => x.Type, "text")
        .RuleFor(x => x.Width, f => f.Random.Number(1, 12))
        .RuleFor(x => x.Margin, _ => MarginFaker.Generate());

    private static readonly Faker<ImageBlock> ImageBlockFaker = new AutoFaker<ImageBlock>()
        .RuleFor(x => x.Content, f => f.Image.LoremPixelUrl())
        .RuleFor(x => x.Type, "image")
        .RuleFor(x => x.Width, f => f.Random.Number(1, 12))
        .RuleFor(x => x.Margin, _ => MarginFaker.Generate());

    private static readonly Faker<TableBlock> TableBlockFaker = new AutoFaker<TableBlock>()
        .RuleFor(x => x.Content,
            f => f.Make(f.Random.Number(1, 5), _ => f.Lorem.Words(f.Random.Number(1, 5))).ToArray())
        .RuleFor(x => x.Type, "table")
        .RuleFor(x => x.Width, f => f.Random.Number(1, 12))
        .RuleFor(x => x.Margin, _ => MarginFaker.Generate());

    private delegate Block GenerateBlock(string? ruleSets = null);

    private static IEnumerable<Block> Generate(
        GenerateBlock generateBlock,
        int count)
    {
        lock (Lock)
        {
            var blocks = new List<Block>();
            for (int i = 0; i < count; i++)
            {
                blocks.Add(generateBlock());
            }

            return blocks;
        }
    }

    public static IEnumerable<Block> GenerateBlocks(int count = 1)
    {
        return Generate(AbstractFaker.Generate, count);
    }

    public static IEnumerable<TextBlock> GenerateTextBlocks(int count = 1)
    {
        return Generate(TextBlockFaker.Generate, count)
            .Select(x => x as TextBlock)!;
    }

    public static IEnumerable<ImageBlock> GenerateImageBlocks(int count = 1)
    {
        return Generate(ImageBlockFaker.Generate, count)
            .Select(x => x as ImageBlock)!;
    }

    public static IEnumerable<TableBlock> GenerateTableBlocks(int count = 1)
    {
        return Generate(TableBlockFaker.Generate, count)
            .Select(x => x as TableBlock)!;
    }
}