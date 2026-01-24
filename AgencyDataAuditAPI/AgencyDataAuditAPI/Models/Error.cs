using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class Error
{
    public long ErrorId { get; set; }

    public long ReportingPeriodFileId { get; set; }

    public long ErrorCategoryId { get; set; }

    public int RecordCount { get; set; }

    public virtual ErrorCategory ErrorCategory { get; set; } = null!;

    public virtual ReportingPeriodFile ReportingPeriodFile { get; set; } = null!;
}
