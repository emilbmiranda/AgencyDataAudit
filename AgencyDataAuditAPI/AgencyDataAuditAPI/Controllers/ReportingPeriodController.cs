using Microsoft.AspNetCore.Mvc;
using AgencyDataAuditAPI.Services;
using AgencyDataAuditAPI.Models;
using System;

namespace AgencyDataAuditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportingPeriodController(IReportingPeriodService reportingPeriodService) : ControllerBase
{
    private readonly IReportingPeriodService ReportingPeriodService = reportingPeriodService;

    [HttpGet(Name = "GetReportingPeriod")]
    public async Task<IEnumerable<ReportingPeriod>> Get()
    {
        var result = await ReportingPeriodService.GetReportingPeriodsAsync();;
        return result;
    }

    [HttpGet("{reportingPeriodId}", Name = "GetReportingPeriodById")]
    public async Task<ReportingPeriod> GetReportingById(int reportingPeriodId)
    {
        var result = await ReportingPeriodService.GetReportingPeriodByIdAsync(reportingPeriodId);
        return result;
    }    
}
