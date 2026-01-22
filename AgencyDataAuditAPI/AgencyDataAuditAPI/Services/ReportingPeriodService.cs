using System.Collections.Generic;
using System;  
using System.Linq;
using System.Threading.Tasks;
using AgencyDataAuditAPI.Models;
using AgencyDataAuditAPI.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;
using System.Net.WebSockets;
using System.Runtime.Versioning;
using System.Text;

namespace AgencyDataAuditAPI.Services;
public interface IReportingPeriodService
{
    Task<List<ReportingPeriod>> GetReportingPeriodsAsync();
    Task<ReportingPeriod> GetReportingPeriodByIdAsync(long reportingPeriodId);
    Task<ReportingPeriod> GetReportingPeriodByReportingPeriodFileIdAsync(long reportingPeriodFileId);
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

    public async Task<ReportingPeriod> GetReportingPeriodByIdAsync(long reportingPeriodId)
    {
        return await _context.ReportingPeriods.FirstOrDefaultAsync(m => m.ReportingPeriodId == reportingPeriodId);
    }

    public async Task<ReportingPeriod> GetReportingPeriodByReportingPeriodFileIdAsync(long reportingPeriodFileId)
    {
        var rpf = await _context.ReportingPeriodFiles.FirstOrDefaultAsync(m => m.ReportingPeriodFileId == reportingPeriodFileId); 
        return await _context.ReportingPeriods.FirstOrDefaultAsync(m => m.ReportingPeriodId == rpf.ReportingPeriodId);
    }
}
