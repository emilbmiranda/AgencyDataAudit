using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class ErrorComparison
{
    public string? ErrorCategoryName { get; set; }
    public long? EquifaxRecordCount { get; set; }
    public long? ExperianRecordCount { get; set; }
    public long? InnovisRecordCount { get; set; }
    public long? TransUnionRecordCount { get; set; }
}
