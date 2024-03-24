using PdfReportsGenerator.Bll.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfReportsGenerator.BackgroundWorker;

public class PdfGenerator
{ 
    public Document Generate(KafkaRecord kafkaRecord)
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
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.ComposeBody(kafkaRecord);
                    });
            });
        });

        return document;
    }
}