using Grpc.Core;
using ReportTasks.V1;

namespace PdfReportsGenerator.Api.Grpc.Services;

public class ReportTasksService : 
    ReportTasks.V1.ReportTasksService.ReportTasksServiceBase
{
    public ReportTasksService()
    {
        
    }

    public override Task<CreateReportTaskResponse> CreateReportTask(
        CreateReportTaskRequest request,
        ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<GetReportTaskStatusResponse> GetReportTaskStatus(
        GetReportTaskStatusRequest request,
        ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}