using PdfReportsGenerator.Core.Entities;
using PdfReportsGenerator.Core.Models;

namespace PdfReportsGenerator.Application.Services.Interfaces;

public interface IReportsService
{
    Task<ReportTask> CreateReport(ReportBody report);
    Task<ReportTask> GetReport(string reportGuid);
    Task<ReportTask> UpdateReport(ReportTask report);
}