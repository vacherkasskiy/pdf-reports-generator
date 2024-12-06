using AutoMapper;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Gateway.Rest.Requests;

namespace PdfReportsGenerator.Gateway.Rest.Builders;

public class ReportBuilder : Profile
{
    public ReportBuilder()
    {
        CreateMap<CreateReportRequest, ReportTaskDto>();
    }
}