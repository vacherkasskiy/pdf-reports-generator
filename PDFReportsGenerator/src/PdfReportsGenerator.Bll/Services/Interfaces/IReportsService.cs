using Report = PdfReportsGenerator.Dal.Entities.Report;

namespace PdfReportsGenerator.Bll.Services.Interfaces;

public interface IReportsService
{
    Task<Report> CreateReport(Models.Report report);
    Task<Report> GetReport(string reportGuid);
    Task<Report> UpdateReport(Report report);
}