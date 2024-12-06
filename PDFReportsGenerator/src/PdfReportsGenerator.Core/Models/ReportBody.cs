using Newtonsoft.Json;
using PdfReportsGenerator.Core.JsonConverters;

namespace PdfReportsGenerator.Core.Models;

public class ReportBody
{
    public string? Name { get; set; }
    
    [JsonConverter(typeof(JsonBlockConverter))]
    public Block?[]? Blocks { get; set; }
}

public class Block
{
    [JsonProperty("type")]
    public virtual string? Type { get; set; }
    public Margin? Margin { get; set; }
    public int? Width { get; set; }
}

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