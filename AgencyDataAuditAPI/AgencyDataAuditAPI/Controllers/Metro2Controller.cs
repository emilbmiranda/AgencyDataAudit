using Microsoft.AspNetCore.Mvc;
using AgencyDataAuditAPI.Services;
using AgencyDataAuditAPI.Models;
using System;

namespace AgencyDataAuditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class Metro2Controller(IMetro2Service metro2Service) : ControllerBase
{
    private readonly IMetro2Service Metro2Service = metro2Service;

    [HttpGet("{reportingPeriodId}", Name = "GetMetro2")]
    public async Task<IEnumerable<Metro2>> Get(int reportingPeriodId)
    {
        var result = await Metro2Service.GetMetro2sAsync(reportingPeriodId);
        return result;
    }

    [HttpGet("GetMetro2Trending/{systemOfRecordId}", Name = "GetMetro2Trending")]
    public async Task<IEnumerable<Metro2Trending>> GetTrending(int systemOfRecordId)
    {
        var result = await Metro2Service.GetMetro2TrendingAsync(systemOfRecordId);
        return result;
    } 

    [HttpGet("{reportingPeriodId}/{account}", Name = "GetMetro2ByAccount")]
    public async Task<Metro2> GetAccount(int reportingPeriodId, string account)
    {
        var result = await Metro2Service.GetMetro2AccountAsync(reportingPeriodId,  account);
        return result;
    }    
}
