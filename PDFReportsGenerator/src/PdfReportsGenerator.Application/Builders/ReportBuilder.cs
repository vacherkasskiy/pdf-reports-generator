using AutoMapper;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Domain.Entities;

namespace PdfReportsGenerator.Application.Builders;

public class ReportBuilder : Profile
{
    public ReportBuilder()
    {
        CreateMap<ReportTaskDto, ReportTask>().ReverseMap();
    }
}