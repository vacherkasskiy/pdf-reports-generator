using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services.Interfaces;

namespace PdfReportsGenerator.Api.Restful.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportTasksController : ControllerBase
{
    private readonly IReportTasksService _service;
    
    public ReportTasksController(IReportTasksService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("/api/v1/reports-tasks")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateReportTask(Report request)
    {
        try
        {
            var reportTask = await _service.CreateReportTask(request);
            return Ok($"Task successfully created with Id: {reportTask.Id}");
        }
        catch (InvalidDataException e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
    
    [HttpGet]
    [Route("/api/v1/reports-tasks/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(202)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetReportTaskStatus(int taskId)
    {
        throw new NotImplementedException();
    }
}