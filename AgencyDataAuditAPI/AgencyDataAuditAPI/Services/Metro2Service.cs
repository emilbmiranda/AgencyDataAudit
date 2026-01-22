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
public interface IMetro2Service
{
    Task<List<Metro2>> GetMetro2sAsync(int reportingPeriodId);
     Task<List<Metro2Trending>> GetMetro2TrendingAsync(int systemOfRecordId);
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
        
        var rpf = await _context.ReportingPeriodFiles.Where(m => m.ReportingPeriodId == reportingPeriodId && m.FileTypeId == 1).FirstOrDefaultAsync();
        var metro2s = await _context.Metro2s.Where(m => m.ReportingPeriodFileId == rpf.ReportingPeriodFileId).ToListAsync();
        return metro2s;
    }

    public async Task<List<Metro2Trending>> GetMetro2TrendingAsync(int systemOfRecordId)
    {
        var result = new List<Metro2Trending>();
        var rps = await _context.ReportingPeriods.Where(m => m.SystemOfRecordId == systemOfRecordId).ToListAsync();
        var rpfs = new List<ReportingPeriodFile>();
        foreach (var rp in rps)
        {
            var rpf = await _context.ReportingPeriodFiles.FirstOrDefaultAsync(m => m.ReportingPeriodId == rp.ReportingPeriodId && m.FileTypeId == 1);
            var metro2Count = _context.Metro2s.Where(m => m.ReportingPeriodFileId == rpf.ReportingPeriodFileId).Count();
            var metro2Trending = new Metro2Trending() { ActivityDate = rp.ActivityDate.ToString("MMM"), RecordCount = metro2Count };
            result.Add(metro2Trending);
        }

        return result;
    }
    public async Task<Metro2> GetMetro2AccountAsync(int reportingPeriodId, string account)
    {
        var rpf = await _context.ReportingPeriodFiles.Where(m => m.ReportingPeriodId == reportingPeriodId && m.FileTypeId == 1).FirstOrDefaultAsync();
        var metro2 = await _context.Metro2s.FirstOrDefaultAsync(m => m.ReportingPeriodFileId == rpf.ReportingPeriodFileId && m.AccountNumber == account);
        return metro2;
    }
}
