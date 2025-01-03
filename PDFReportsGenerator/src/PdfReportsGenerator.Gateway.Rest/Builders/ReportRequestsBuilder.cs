using AutoMapper;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Gateway.Rest.Requests;

namespace PdfReportsGenerator.Gateway.Rest.Builders;

public class ReportRequestsBuilder : Profile
{
    public ReportRequestsBuilder()
    {
        CreateMap<CreateReportRequest, ReportTaskDto>();
    }
}