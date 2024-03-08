using Grpc.Core;
using PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;
using PdfReportsGenerator.Bll.Services.Interfaces;
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
            Message = response.Status.ToString(),
            Status = GetReportResponse.Types.Status.Fulfilled
        };
    }
}