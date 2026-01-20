using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class Metro2
{
    public long Metro2Id { get; set; }

    public long ReportingPeriodFileId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public virtual ReportingPeriodFile ReportingPeriodFile { get; set; } = null!;
}
