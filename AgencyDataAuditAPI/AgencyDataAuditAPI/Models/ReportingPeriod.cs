using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class ReportingPeriod
{
    public long ReportingPeriodId { get; set; }

    public long SystemOfRecordId { get; set; }

    public DateTime ActivityDate { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ReportingPeriodFile? ReportingPeriodFile { get; set; }

    public virtual SystemOfRecord SystemOfRecord { get; set; } = null!;
}
