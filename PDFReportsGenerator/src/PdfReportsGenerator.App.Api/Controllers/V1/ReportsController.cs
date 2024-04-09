using Microsoft.AspNetCore.Mvc;
using PdfReportsGenerator.App.Bll.Services.Interfaces;
using PdfReportsGenerator.Dal.Entities;

namespace PdfReportsGenerator.App.Api.Controllers.V1;

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
    public async Task<ActionResult<Report[]>> GetReports()
    {
        return await _service.GetReports();
    }
}