using Report = PdfReportsGenerator.Dal.Entities.Report;

namespace PdfReportsGenerator.Bll.Services.Interfaces;

public interface IReportsService
{
    Task<Report> CreateReportTask(Models.Report report);
}