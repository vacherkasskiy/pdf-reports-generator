using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PdfReportsGenerator.Dal.Entities;

public class Report
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public Guid Id { get; set; }

    public ReportStatus Status { get; set; }
    
    [MaxLength(256)]
    public string? Link { get; set; }
}

public enum ReportStatus
{
    NotStarted,
    Processing,
    Ready,
    Error,
}