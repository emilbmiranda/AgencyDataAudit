using System.Collections.Generic;
using System;  
using System.Linq;
using System.Threading.Tasks;
using AgencyDataAuditAPI.Models;
using AgencyDataAuditAPI.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Runtime.Versioning;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Reflection;
using System.Diagnostics;

namespace AgencyDataAuditAPI.Services;
public interface IStatisticService
{
    Task<List<StatisticComparison>> GetStatisticComparisonAsync(int reportingPeriodId);
}

public class StatisticService : IStatisticService
{
    private readonly AppDbContext _context;
    private readonly IReportingPeriodService _reportingPeriodService;

    public StatisticService(AppDbContext context, IReportingPeriodService reportingPeriodService)
    {
        _context = context;
        _reportingPeriodService = reportingPeriodService;
    }

    public async Task<List<StatisticComparison>> GetStatisticComparisonAsync(int reportingPeriodId)
    {
        var result = new List<StatisticComparison>();
        var metro2Rpf = await _context.ReportingPeriodFiles.Where(m => m.ReportingPeriodId == reportingPeriodId && m.FileTypeId == 1).FirstOrDefaultAsync();
        var metro2 = await _context.Metro2s.Where(m => m.ReportingPeriodFileId == metro2Rpf.ReportingPeriodFileId).ToListAsync();
        var statisticComparisonMetro2 = new StatisticComparison()
        {
            Source = "Metro2",
            RecordCount = metro2.Count(),
        };
        result.Add(statisticComparisonMetro2);
        var rpfs = await _context.ReportingPeriodFiles.Where(m => m.ReportingPeriodId == reportingPeriodId && m.FileTypeId == 2).ToListAsync();
        foreach (var rpf in rpfs)
        {           
            var statistic = await _context.Statistics.Where(m => m.ReportingPeriodFileId == rpf.ReportingPeriodFileId).FirstOrDefaultAsync();
            var consumerReportingAgency = await _context.ConsumerReportingAgencies.FirstOrDefaultAsync(m => m.ConsumerReportingAgencyId == rpf.ConsumerReportingAgencyId);
            var statisticComparison = new StatisticComparison()
            {
                Source = consumerReportingAgency.Name,
                RecordCount = statistic.RecordCount,
            };
            result.Add(statisticComparison);
        }

        return result;
    }
}
