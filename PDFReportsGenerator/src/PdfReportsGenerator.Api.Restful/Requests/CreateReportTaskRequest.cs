namespace PdfReportsGenerator.Api.Restful.Requests;

public class CreateReportTaskRequest
{
    public string? Name { get; set; }
    public Block[]? Blocks { get; set; }
}

public class Block
{
    public string? Type { get; set; }
    public WidthProperties? Location { get; set; }

    public class WidthProperties
    {
        public int Left { get; set; }
        public int Right { get; set; }
    }
}

public class TextBlock : Block
{
    public string? Content { get; set; }
    public Properties? Style { get; set; }
    
    public class Properties
    {
        public Alignment? Position { get; set; }
        public int Size { get; set; }
        
        public enum Alignment {
            Center,
            Left,
            Right,
        }
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