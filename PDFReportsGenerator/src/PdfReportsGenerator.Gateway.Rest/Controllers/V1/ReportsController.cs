using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Gateway.Rest.Responses;

namespace PdfReportsGenerator.Gateway.Rest.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _service;
    
    public ReportsController(IReportsService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("/api/v1/reports")]
    public async Task<ActionResult<PostReportResponse>> Post(ReportBody request)
    {
        var reportTask = await _service.CreateReport(request);
        var response = new PostReportResponse($"Task successfully created with Id: {reportTask.Id}");
        
        return Ok(response);
    }
    
    [HttpGet]
    [Route("/api/v1/reports/{id}")]
    public async Task<ActionResult<GetReportResponse>> GetById(string id)
    {
        var response = await _service.GetReport(id);
        
        return Ok(new GetReportResponse(
            response.Status.ToString(),
            response.Link ?? "Not ready yet."));
    }
}