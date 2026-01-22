using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class StatisticComparison
{
    public string Source { get; set; }
    public long? RecordCount { get; set; }
}
