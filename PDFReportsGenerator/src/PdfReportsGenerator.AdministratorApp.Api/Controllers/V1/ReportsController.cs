using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.AdministratorApp.Bll.Services.Interfaces;
using PdfReportsGenerator.Domain.Entities;

namespace PdfReportsGenerator.AdministratorApp.Api.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _service;

    public ReportsController(IReportsService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("/app/v1/reports/")]
    public async Task<ActionResult<ReportTask[]>> GetReports()
    {
        return await _service.GetReports();
    }
    
    [HttpPatch]
    [Route("/app/v1/reports/regenerate/{id}")]
    public async Task<ActionResult<string>> RegenerateReport(string id)
    {
        var result = await _service.RegenerateReport(id);
        if (result)
        {
            return "Task regenerated successfully.";
        }

        return "Something went wrong.";
    }
    
    [HttpDelete]
    [Route("/app/v1/reports/delete/{id}")]
    public async Task<IActionResult> DeleteReport(string id)
    {
        await _service.DeleteReport(id);

        return Ok();
    }
}