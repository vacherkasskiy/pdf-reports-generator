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
    public async Task<IActionResult> Post(Report request)
    {
        var reportTask = await _service.CreateReport(request);
        return Ok($"Task successfully created with Id: {reportTask.Id}");
    }
    
    [HttpGet]
    [Route("/api/v1/reports/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _service.GetReport(id);
        return Ok(response);
    }
}