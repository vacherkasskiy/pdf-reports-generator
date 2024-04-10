namespace PdfReportsGenerator.Dal.Models;

public class KafkaRecord
{
    public Guid TaskId { get; set; }
    
    public ReportBody ReportBody { get; set; }
}