using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Converters;

public class JsonReportBlockConverter : JsonConverter<Block[]>
{
    public override Block[] ReadJson(
        JsonReader reader, 
        Type objectType, 
        Block[]? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonArray = JArray.Load(reader);
        var blocks = new List<Block>();
        
        foreach (var jsonObject in jsonArray)
        {
            var type = jsonObject["type"]?.ToString();
            
            Block block = type switch
            {
                "text" => new TextBlock(),
                "image" => new ImageBlock(),
                "table" => new TableBlock(),
                _ => throw new NotSupportedException($"Block type '{type}' is not supported")
            };
            
            serializer.Populate(jsonObject.CreateReader(), block);
            blocks.Add(block);
        }
        
        return blocks.ToArray();
    }

    public override void WriteJson(JsonWriter writer, Block[]? value, JsonSerializer serializer)
    {
        // Ignore.
        throw new NotImplementedException();
    }
}
