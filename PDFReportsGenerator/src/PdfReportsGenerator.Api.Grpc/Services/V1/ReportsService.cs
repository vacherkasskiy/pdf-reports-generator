using Grpc.Core;
using PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal.Entities;
using Reports.V1;
using ReportProto = Reports.V1.CreateReportRequest;
using Report = PdfReportsGenerator.Bll.Models.Report;

namespace PdfReportsGenerator.Api.Grpc.Services.V1;

public class ReportsService :
    Reports.V1.ReportsService.ReportsServiceBase
{
    private readonly IReportsService _service;
    private readonly IParser<ReportProto, Report> _reportsParser;

    public ReportsService(
        IReportsService service,
        IParser<ReportProto, Report> reportsParser)
    {
        _service = service;
        _reportsParser = reportsParser;
    }

    public override async Task<CreateReportResponse> CreateReport(
        CreateReportRequest request,
        ServerCallContext context)
    {
        var report = _reportsParser.Parse(request);
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
            Link = response.Link ?? "Not ready yet.",
            Status = response.Status.ToString()
        };
    }
}