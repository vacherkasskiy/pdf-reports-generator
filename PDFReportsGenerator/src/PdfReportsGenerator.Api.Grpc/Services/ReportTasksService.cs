using Grpc.Core;
using ReportTasks.V1;

namespace PdfReportsGenerator.Api.Grpc.Services;

public class ReportTasksService : 
    ReportTasks.V1.ReportTasksService.ReportTasksServiceBase
{
    private readonly ILogger<ReportTasksService> _logger;

    public ReportTasksService(ILogger<ReportTasksService> logger)
    {
        _logger = logger;
    }

    public override Task<CreateReportTaskResponse> CreateReportTask(
        CreateReportTaskRequest request,
        ServerCallContext context)
    {
        return base.CreateReportTask(request, context);
    }

    public override Task<GetReportTaskStatusResponse> GetReportTaskStatus(
        GetReportTaskStatusRequest request,
        ServerCallContext context)
    {
        return base.GetReportTaskStatus(request, context);
    }
}