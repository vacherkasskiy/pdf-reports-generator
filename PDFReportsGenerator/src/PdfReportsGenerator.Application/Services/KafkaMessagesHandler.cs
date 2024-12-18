using AutoMapper;
using PdfReportsGenerator.Application.Infrastructure.Hubs;
using PdfReportsGenerator.Application.Infrastructure.Minio;
using PdfReportsGenerator.Application.Infrastructure.PdfGenerator;
using PdfReportsGenerator.Application.Infrastructure.Persistence;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services.Interfaces;

namespace PdfReportsGenerator.Application.Services;

public class KafkaMessagesHandler(
    IPdfGeneratorDbContext dbContext,
    IPdfReportHubContext hubContext,
    IPdfReportMinioClient minioClient,
    IPdfGenerator pdfGenerator,
    IMapper mapper) : IKafkaMessagesHandler
{
    public async Task HandleAsync(ReportTaskDto reportTask)
    {
    }
}