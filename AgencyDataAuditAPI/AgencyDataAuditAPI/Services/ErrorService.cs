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
public interface IErrorService
{
    Task<List<ErrorComparison>> GetErrorComparisonsAsync(int reportingPeriodId);
}

public class ErrorService : IErrorService
{
    private readonly AppDbContext _context;

    public ErrorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ErrorComparison>> GetErrorComparisonsAsync(int reportingPeriodId)
    {
        var errorComparisons = new List<ErrorComparison>();
        var comparisonCategories = await _context.ErrorCategories.ToListAsync();
        var rpfs = await _context.ReportingPeriodFiles.Where(m => m.ReportingPeriodId == reportingPeriodId && m.FileTypeId == 3).ToListAsync();
        foreach(var cc in comparisonCategories)
        {
            var errors = await _context.Errors.Include(m => m.ReportingPeriodFile).Where(m => m.ErrorCategoryId == cc.ErrorCategoryId).Select(m => new { m.ReportingPeriodFile.ConsumerReportingAgencyId, m.RecordCount }).ToListAsync();
            var ec = new ErrorComparison()
            {
                ErrorCategoryName = cc.ErrorCategoryName,
                EquifaxRecordCount = errors.Where(m => m.ConsumerReportingAgencyId == 1).FirstOrDefault()?.RecordCount ?? 0,
                ExperianRecordCount = errors.Where(m => m.ConsumerReportingAgencyId == 2).FirstOrDefault()?.RecordCount ?? 0,
                InnovisRecordCount = errors.Where(m => m.ConsumerReportingAgencyId == 3).FirstOrDefault()?.RecordCount ?? 0,
                TransUnionRecordCount = errors.Where(m => m.ConsumerReportingAgencyId == 4).FirstOrDefault()?.RecordCount ?? 0,
            };
            errorComparisons.Add(ec);
        }
        return errorComparisons;
    }
}
