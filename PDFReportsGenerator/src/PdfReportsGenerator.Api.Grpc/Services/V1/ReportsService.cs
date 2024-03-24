using Grpc.Core;
using PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal.Entities;
using Reports.V1;

namespace PdfReportsGenerator.Api.Grpc.Services.V1;

public class ReportsService :
    Reports.V1.ReportsService.ReportsServiceBase
{
    private readonly IReportsService _service;
    private readonly IReportsParser _parser;
    
    public ReportsService(
        IReportsService service,
        IReportsParser parser)
    {
        _service = service;
        _parser = parser;
    }

    public override async Task<CreateReportResponse> CreateReport(
        CreateReportRequest request,
        ServerCallContext context)
    {
        var report = _parser.ParseReport(request);
        var reportTask = await _service.CreateReport(report);
        return new CreateReportResponse
        {
            Id = reportTask.Id.ToString()
        };
    }

    public override async Task<GetReportResponse> GetReport(
        GetReportRequest request,
        ServerCallContext context)
    {
        var response = await _service.GetReport(request.Id);
        return new GetReportResponse
        {
            Message = response.Link,
            Status = ParseProtoStatus(response.Status)
        };
    }

    private GetReportResponse.Types.Status ParseProtoStatus(Dal.Entities.ReportStatus status)
    {
        switch (status)
        {
            case ReportStatus.NotStarted:
                return GetReportResponse.Types.Status.NotStarted;
            case ReportStatus.Processing:
                return GetReportResponse.Types.Status.Processing;
            case ReportStatus.Ready:
                return GetReportResponse.Types.Status.Ready;
            case ReportStatus.Error:
                return GetReportResponse.Types.Status.Error;
            default:
                throw new Exception("Unknown status provided.");
        }
    }
}