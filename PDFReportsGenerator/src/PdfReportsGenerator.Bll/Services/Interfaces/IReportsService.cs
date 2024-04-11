using PdfReportsGenerator.Dal.Entities;
using PdfReportsGenerator.Dal.Models;

namespace PdfReportsGenerator.Bll.Services.Interfaces;

public interface IReportsService
{
    Task<ReportTask> CreateReport(ReportBody report);
    Task<ReportTask> GetReport(string reportGuid);
    Task<ReportTask> UpdateReport(ReportTask report);
}