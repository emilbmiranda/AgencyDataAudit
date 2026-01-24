using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class ConsumerReportingAgency
{
    public long ConsumerReportingAgencyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReportingPeriodFile> ReportingPeriodFiles { get; set; } = new List<ReportingPeriodFile>();
}
