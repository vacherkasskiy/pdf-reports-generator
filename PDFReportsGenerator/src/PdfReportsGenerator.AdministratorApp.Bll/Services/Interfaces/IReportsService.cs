using PdfReportsGenerator.Domain.Entities;

namespace PdfReportsGenerator.AdministratorApp.Bll.Services.Interfaces;

public interface IReportsService
{
    public Task<ReportTask[]> GetReports();
    public Task DeleteReport(string id);
    public Task<bool> RegenerateReport(string id);
}