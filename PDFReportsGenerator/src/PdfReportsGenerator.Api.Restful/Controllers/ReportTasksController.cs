using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Api.Restful.Responses;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Validators.Interfaces;

namespace PdfReportsGenerator.Api.Restful.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportTasksController : ControllerBase
{
    private readonly IValidator<Report> _validator;

    public ReportTasksController(IValidator<Report> validator)
    {
        _validator = validator;
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
            if (!_validator.IsValid(request))
                return StatusCode(
                    StatusCodes.Status400BadRequest, 
                    "Invalid report provided");

            var response = new CreateReportTaskResponse(TaskId: 1);

            return Ok(response);
        }
        catch
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError, 
                "Something went wrong");
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