using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Services.Interfaces;

public interface IKafkaMessagesHandler
{
    Task HandleAsync(ReportTaskDto reportTask);
}