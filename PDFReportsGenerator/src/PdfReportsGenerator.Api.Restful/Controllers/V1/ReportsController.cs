using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Api.Restful.Responses;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal.Models;

namespace PdfReportsGenerator.Api.Restful.Controllers.V1;

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
    public async Task<ActionResult<string>> Post([FromBody]ReportBody request)
    {
        var reportTask = await _service.CreateReport(request);
        return Ok($"Task successfully created with Id: {reportTask.Id}");
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