using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;

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
    public async Task<IActionResult> CreateReport(Report request)
    {
        var reportTask = await _service.CreateReport(request);
        return Ok($"Task successfully created with Id: {reportTask.Id}");
    }
    
    [HttpGet]
    [Route("/api/v1/reports/{id}")]
    public async Task<IActionResult> GetReport(ulong reportId)
    {
        var response = await _service.GetReport(reportId);
        return Ok(response);
    }
}