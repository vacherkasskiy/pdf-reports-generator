using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.Api.Restful.Requests;
using PdfReportsGenerator.Api.Restful.Responses;

namespace PdfReportsGenerator.Api.Restful.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportTasksController : ControllerBase
{
    public ReportTasksController()
    {
        
    }

    [HttpPost]
    [Route("/api/v1/reports-tasks")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<CreateReportTaskResponse> CreateReportTask(CreateReportTaskRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("/api/v1/reports-tasks/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(202)]
    [ProducesResponseType(500)]
    public async Task<GetReportTaskStatusResponse> GetReportTaskStatus(int taskId)
    {
        throw new NotImplementedException();
    }
}