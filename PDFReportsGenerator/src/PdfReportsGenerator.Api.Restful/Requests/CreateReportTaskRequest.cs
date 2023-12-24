using Newtonsoft.Json;
using PdfReportsGenerator.Api.Restful.Converters;

namespace PdfReportsGenerator.Api.Restful.Requests;

public class CreateReportTaskRequest
{
    public string? Name { get; set; }
    
    [JsonConverter(typeof(BlockConverter))]
    public Block[]? Blocks { get; set; }
}

public class Block
{
    public string? Type { get; set; }
    public Location? Location { get; set; }
}

public class Location
{
    public int Left { get; set; }
    public int Right { get; set; }
}

public class TextBlock : Block
{
    public string? Content { get; set; }
    public Style? Style { get; set; }
}

public class Style
{
    public Alignment? Position { get; set; }
    public int Size { get; set; }
        
    public enum Alignment {
        Center,
        Left,
        Right,
    }
}

public class ImageBlock : Block
{
    string? Content { get; set; }
}

public class TableBlock : Block
{
    public string[][]? Content { get; set; }
}