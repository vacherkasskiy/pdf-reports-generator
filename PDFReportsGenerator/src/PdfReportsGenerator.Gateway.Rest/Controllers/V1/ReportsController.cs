using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Newtonsoft.Json;
using PdfReportsGenerator.Application.Converters;
using PdfReportsGenerator.Application.Helpers.Interfaces;
using PdfReportsGenerator.Application.Infrastructure.Minio;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Gateway.Rest.Requests;
using PdfReportsGenerator.Gateway.Rest.Responses;
using PdfReportsGenerator.Infrastructure.PdfGenerator.Interfaces;

namespace PdfReportsGenerator.Gateway.Rest.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportTaskService _service;
    private readonly IMapper _mapper;

    public ReportsController(IReportTaskService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("/api/v1/reports")]
    public async Task<ActionResult<PostReportResponse>> Post(CreateReportRequest request)
    {
        var reportModel = _mapper.Map<ReportTaskDto>(request);
        var reportTaskId = await _service.CreateReportAsync(reportModel);
        var response = new PostReportResponse($"Task successfully created with Id: {reportTaskId}");

        return Ok(response);
    }

    [HttpGet]
    [Route("/api/v1/reports/{id}")]
    public async Task<ActionResult<GetReportResponse>> GetById(string id)
    {
        var response = await _service.GetReportAsync(id);

        return Ok(new GetReportResponse(
            response.Status.ToString(),
            response.ReportS3Link ?? "Not ready yet."));
    }
}