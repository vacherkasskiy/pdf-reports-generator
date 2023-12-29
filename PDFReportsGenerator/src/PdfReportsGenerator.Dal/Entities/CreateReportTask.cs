using System.ComponentModel.DataAnnotations;

namespace PdfReportsGenerator.Dal.Entities;

public class CreateReportTask
{
    [Key]
    public long Id { get; set; }
    public string? Report { get; set; }
}