using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PdfReportsGenerator.Domain.Entities;

public class ReportTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string ReportBody { get; set; }
    
    public string AuthorName { get; set; }
    
    public string ReportName { get; set; }

    public ReportStatus Status { get; set; }

    [MaxLength(256)] public string? Link { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public enum ReportStatus
{
    NotStarted,
    Processing,
    Ready,
    InvalidTemplate,
    Error
}