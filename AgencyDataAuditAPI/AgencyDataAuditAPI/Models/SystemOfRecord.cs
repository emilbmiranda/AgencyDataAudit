using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class SystemOfRecord
{
    public long SystemOfRecordId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ReportingPeriod> ReportingPeriods { get; set; } = new List<ReportingPeriod>();
}
