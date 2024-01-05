using System.ComponentModel.DataAnnotations;

namespace PdfReportsGenerator.Dal.Entities;

public class Report
{
    [Key]
    public ulong Id { get; set; }
    public string Body { get; set; }
}