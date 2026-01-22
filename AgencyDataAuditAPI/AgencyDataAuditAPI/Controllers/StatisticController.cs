using Microsoft.AspNetCore.Mvc;
using AgencyDataAuditAPI.Services;
using AgencyDataAuditAPI.Models;
using System;

namespace AgencyDataAuditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StatisticController(IStatisticService statisticService) : ControllerBase
{
    private readonly IStatisticService StatisticService = statisticService;

    [HttpGet("{reportingPeriodId}", Name = "GetStatisticComparisonAsync")]
    public async Task<List<StatisticComparison>> GetStatisticComparisonAsync(int reportingPeriodId)
    {
        var result = await StatisticService.GetStatisticComparisonAsync(reportingPeriodId);
        return result;
    }    
}
