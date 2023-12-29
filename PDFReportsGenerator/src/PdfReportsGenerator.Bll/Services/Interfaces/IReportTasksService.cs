using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.Bll.Services.Interfaces;

public interface IReportTasksService
{
    Task<CreateReportTask> CreateReportTask(Report report);
}