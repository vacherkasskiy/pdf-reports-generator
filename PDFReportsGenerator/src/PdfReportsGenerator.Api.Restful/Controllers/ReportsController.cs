using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Bll.Exceptions;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;

namespace PdfReportsGenerator.Api.Restful.Controllers;

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
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateReport(Report request)
    {
        try
        {
            var reportTask = await _service.CreateReportTask(request);
            return Ok($"Task successfully created with Id: {reportTask.Id}");
        }
        catch (InvalidReportFormatException _)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid report format");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
    
    [HttpGet]
    [Route("/api/v1/reports/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(202)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetReport(int taskId)
    {
        throw new NotImplementedException();
    }
}