using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.App.Bll.Services.Interfaces;

public interface IReportsService
{
    public Task<Report[]> GetReports();
    public Task DeleteReport(Guid id);
    public Task RegenerateReport(Guid id);
}