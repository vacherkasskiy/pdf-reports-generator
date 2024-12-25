using Newtonsoft.Json;
using PdfReportsGenerator.Application.Converters;
using PdfReportsGenerator.Application.Helpers.Interfaces;
using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Helpers;

public class PdfParser : IPdfParser
{
    public ReportObject ParseToObject(ReportTaskDto task)
    {
        if (string.IsNullOrWhiteSpace(task.ReportBody))
        {
            throw new ArgumentException("ReportBody cannot be null or empty", nameof(task));
        }

        try
        {
            var blocks = JsonConvert.DeserializeObject<Block[]>(
                task.ReportBody,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.None,
                    Converters = { new JsonReportBlockConverter() }
                });

            if (blocks == null)
            {
                throw new JsonSerializationException("Failed to deserialize ReportBody to blocks.");
            }

            return new ReportObject
            {
                Id = task.Id,
                Name = task.ReportName,
                Blocks = blocks
            };
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to parse ReportBody from the task.", ex);
        }
    }
}