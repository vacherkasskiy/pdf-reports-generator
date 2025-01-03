using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services.Interfaces;
using Reports.V1;

namespace PdfReportsGenerator.Gateway.Grpc.Services.V1;

public class ReportsService(IReportTaskService service, IMapper mapper) :
    Reports.V1.ReportsService.ReportsServiceBase
{
    public override async Task<CreateReportResponse> CreateReport(
        CreateReportRequest request,
        ServerCallContext context)
    {
        var reportTask = await service.CreateReportAsync(mapper.Map<ReportTaskDto>(request));
        
        return new CreateReportResponse
        {
            ReportTaskId = reportTask.ToString()
        };
    }

    public override async Task<GetReportResponse> GetReport(
        GetReportRequest request,
        ServerCallContext context)
    {
        var response = await service.GetReportAsync(request.ReportTaskId);
        
        return new GetReportResponse
        {
            Link = response.ReportS3Link ?? "Not ready yet",
            Status = response.Status.ToString()
        };
    }

    public override async Task<GetReportsResponse> GetReports(
        Empty request, 
        ServerCallContext context)
    {
        var data = await service.GetReportsAsync();
        var response = new GetReportsResponse();
        
        foreach (var report in mapper.Map<List<ReportTaskInfo>>(data))
        {
            response.ReportTasks.Add(report);
        }

        return response;
    }

    public override async Task<Empty> DeleteReport(
        DeleteReportRequest request, 
        ServerCallContext context)
    {
        await service.DeleteReportAsync(Guid.Parse(request.ReportTaskId));
        
        return new Empty();
    }
}