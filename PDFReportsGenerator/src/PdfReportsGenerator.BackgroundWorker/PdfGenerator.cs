using PdfReportsGenerator.Dal.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.BackgroundWorker;

public class PdfGenerator : IDisposable
{
    private readonly KafkaRecord _kafkaRecord;
    private readonly string _fileName;
    
    public PdfGenerator(KafkaRecord kafkaRecord)
    {
        _kafkaRecord = kafkaRecord;
        _fileName = _kafkaRecord.TaskId.ToString();
    }
    
    /// <summary>
    /// Generates PDF report for following KafkaRecord with the name of it's ID.
    /// </summary>
    /// <returns> Name of the file with PDF report. </returns>
    public string Generate()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Content()
                    .Grid(grid =>
                    {
                        grid.VerticalSpacing(10);
                        grid.HorizontalSpacing(10);
                        
                        grid.ComposeBody(_kafkaRecord);
                    });
            });
        });
        
        document.GeneratePdf(_fileName);

        return _fileName;
    }

    public void Dispose()
    {
        if (File.Exists(_fileName))
        {
            File.Delete(_fileName);
        }
    }
}