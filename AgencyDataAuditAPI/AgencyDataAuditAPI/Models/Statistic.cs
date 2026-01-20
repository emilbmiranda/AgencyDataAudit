using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class Statistic
{
    public long StatisticId { get; set; }

    public long ReportingPeriodFileId { get; set; }

    public int? RecordCount { get; set; }

    public virtual ReportingPeriodFile ReportingPeriodFile { get; set; } = null!;
}
