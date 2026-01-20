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
public interface IMetro2Service
{
    Task<List<Metro2>> GetMetro2sAsync(int reportingPeriodId);
    Task<Metro2> GetMetro2AccountAsync(int reportingPeriodId, string account);
}

public class Metro2Service : IMetro2Service
{
    private readonly AppDbContext _context;

    public Metro2Service(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Metro2>> GetMetro2sAsync(int reportingPeriodId)
    {
        
        var rpf = await _context.ReportingPeriodFiles.Where(m => m.ReportingPeriodId == reportingPeriodId).FirstOrDefaultAsync();
        var metro2s = await _context.Metro2s.Where(m => m.ReportingPeriodFileId == 1).ToListAsync();
        return metro2s;
    }
    public async Task<Metro2> GetMetro2AccountAsync(int reportingPeriodId, string account)
    {
        var rpf = await _context.ReportingPeriodFiles.Where(m => m.ReportingPeriodId == reportingPeriodId).FirstOrDefaultAsync();
        var metro2 = await _context.Metro2s.FirstOrDefaultAsync(m => m.ReportingPeriodFileId == rpf.ReportingPeriodFileId && m.AccountNumber == account);
        return metro2;
    }
}
