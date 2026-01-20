using System.Collections.Generic;
using System;  
using System.Linq;
using System.Threading.Tasks;
using AgencyDataAuditAPI.Models;
using AgencyDataAuditAPI.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace AgencyDataAuditAPI.Services;
public interface IReportingPeriodService
{
    Task<List<ReportingPeriod>> GetReportingPeriodsAsync();
    Task<ReportingPeriod> GetReportingPeriodByIdAsync(int reportingPeriodId);
}

public class ReportingPeriodService : IReportingPeriodService
{
    private readonly AppDbContext _context;

    public ReportingPeriodService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReportingPeriod>> GetReportingPeriodsAsync()
    {
        return await _context.ReportingPeriods.ToListAsync();
    }

    public async Task<ReportingPeriod> GetReportingPeriodByIdAsync(int reportingPeriodId)
    {
        return await _context.ReportingPeriods.FirstOrDefaultAsync(m => m.ReportingPeriodId == reportingPeriodId);
    }
}
