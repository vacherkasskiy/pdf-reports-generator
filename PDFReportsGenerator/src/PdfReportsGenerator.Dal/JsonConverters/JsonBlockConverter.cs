using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfReportsGenerator.Dal.Models;

namespace PdfReportsGenerator.Dal.JsonConverters;

public class JsonBlockConverter : JsonConverter<Block[]>
{
    public override Block[] ReadJson(
        JsonReader reader,
        Type objectType,
        Block[]? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var array = JArray.Load(reader);
        var blocks = new List<Block?>();

        foreach (var jObject in array.Children<JObject>())
        {
            if (!jObject.TryGetValue("type", out JToken? typeToken))
            {
                blocks.Add(null);
                continue;
            }

            var type = typeToken.Value<string>();
            switch (type)
            {
                case "text":
                    blocks.Add(jObject.ToObject<TextBlock>(serializer));
                    break;
                case "image":
                    blocks.Add(jObject.ToObject<ImageBlock>(serializer));
                    break;
                case "table":
                    blocks.Add(jObject.ToObject<TableBlock>(serializer));
                    break;
                default:
                    blocks.Add(null);
                    break;
            }
        }

        return blocks.ToArray()!;
    }

    public override void WriteJson(
        JsonWriter writer,
        Block[]? value,
        JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteStartArray();

        foreach (var block in value)
        {
            var type = block.Type;
            switch (type)
            {
                case "text":
                    serializer.Serialize(writer, block as TextBlock, typeof(TextBlock));
                    break;
                case "image":
                    serializer.Serialize(writer, block as ImageBlock, typeof(ImageBlock));
                    break;
                case "table":
                    serializer.Serialize(writer, block as TableBlock, typeof(TableBlock));
                    break;
            }
        }

        writer.WriteEndArray();
    }
}
