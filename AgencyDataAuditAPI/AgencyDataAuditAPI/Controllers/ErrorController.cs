using Microsoft.AspNetCore.Mvc;
using AgencyDataAuditAPI.Services;
using AgencyDataAuditAPI.Models;
using System;

namespace AgencyDataAuditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorController(IErrorService errorService) : ControllerBase
{
    private readonly IErrorService ErrorService = errorService;

    [HttpGet("{reportingPeriodId}", Name = "GetErrorComparisonsAsync")]
    public async Task<List<ErrorComparison>> GetErrorComparisonsAsync(int reportingPeriodId)
    {
        var result = await ErrorService.GetErrorComparisonsAsync(reportingPeriodId);
        return result;
    }    
}
