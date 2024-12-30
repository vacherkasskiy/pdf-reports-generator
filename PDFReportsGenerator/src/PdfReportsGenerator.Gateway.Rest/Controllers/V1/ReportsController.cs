using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Gateway.Rest.Requests;
using PdfReportsGenerator.Gateway.Rest.Responses;

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
    
    [HttpGet]
    [Route("/api/v1/reports/")]
    public async Task<ActionResult<ReportTaskDto[]>> GetReports()
    {
        return await _service.GetReportsAsync();
    }

    [HttpDelete]
    [Route("/api/v1/reports/delete/{id}")]
    public async Task<IActionResult> DeleteReport(Guid id)
    {
        await _service.DeleteReportAsync(id);

        return Ok();
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
    
    [HttpGet("/api/v1/reports/download/{fileName}")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        try
        {
            Response.ContentType = "application/pdf";
            Response.Headers.Append("Content-Disposition", $"attachment; filename=\"{fileName}\".pdf");
            
            await _service.DownloadReportAsync(fileName, Response.Body);

            return new EmptyResult();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}