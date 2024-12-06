using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Domain.Entities;

namespace PdfReportsGenerator.Application.Services.Interfaces;

public interface IReportsService
{
    Task<ReportTask> CreateReport(ReportBody report);
    Task<ReportTask> GetReport(string reportGuid);
    Task<ReportTask> UpdateReport(ReportTask report);
}