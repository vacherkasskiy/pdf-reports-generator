namespace PdfReportsGenerator.Application.Models;

public record ReportObject
{
    public Guid Id { get; init; }
    
    public required string AuthorName { get; init; }

    public required string ReportName { get; init; }
    
    public Block?[]? Blocks { get; init; }
}

public class Block
{
    public virtual string? Type { get; set; }
    public Margin? Margin { get; set; }
    public int? Width { get; set; }
}

#region Secondary classes

public class Margin
{
    public int? Top { get; set; }
    public int? Bottom { get; set; }
    public int? Left { get; set; }
    public int? Right { get; set; }
}

public class TextBlock : Block
{
    public override string Type => "text";
    public string? Content { get; set; }
    public Style? Style { get; set; }
}

public class ImageBlock : Block
{
    public override string Type => "image";
    public string? Content { get; set; }
}

public class TableBlock : Block
{
    public override string Type => "table";
    public string?[]?[]? Content { get; set; }
}

public class Style
{
    public string? Position { get; set; }
    public int Size { get; set; }
}

#endregion