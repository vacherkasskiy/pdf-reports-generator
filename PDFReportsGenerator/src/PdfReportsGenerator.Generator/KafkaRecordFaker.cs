using PdfReportsGenerator.Bll.Models;

namespace PdfReportsGenerator.Generator;

/// <summary>
/// TODO: Delete this.
/// WILL NOT PRESENT IN PRODUCTION VERSION.
/// </summary>
public static class KafkaRecordFaker
{
    public static KafkaRecord GetKafkaRecord()
    {
        return new KafkaRecord
        (
            Guid.NewGuid(),
            ReportFaker.GetReport()
        );
    }
}