using AutoMapper;
using PdfReportsGenerator.Application.Models;
using Reports.V1;

namespace PdfReportsGenerator.Gateway.Grpc.Builders;

public class ReportRequestsBuilder : Profile
{
    public ReportRequestsBuilder()
    {
        CreateMap<CreateReportRequest, ReportTaskDto>();
        CreateMap<ReportTaskInfo, ReportTaskDto>().ReverseMap();
    }
}