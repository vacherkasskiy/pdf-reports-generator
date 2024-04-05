namespace PdfReportsGenerator.Bll.Models;

public class KafkaRecord
{
    public Guid TaskId { get; set; }
    
    public Report Report { get; set; }
}