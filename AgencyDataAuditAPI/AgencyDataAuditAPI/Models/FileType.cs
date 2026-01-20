using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class FileType
{
    public long FileTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReportingPeriodFile> ReportingPeriodFiles { get; set; } = new List<ReportingPeriodFile>();
}
