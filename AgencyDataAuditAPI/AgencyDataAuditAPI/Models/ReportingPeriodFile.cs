using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class ReportingPeriodFile
{
    public long ReportingPeriodFileId { get; set; }

    public long ReportingPeriodId { get; set; }

    public long? ConsumerReportingAgencyId { get; set; }

    public long FileTypeId { get; set; }

    public long FileStateId { get; set; }

    public string? Filename { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual ConsumerReportingAgency? ConsumerReportingAgency { get; set; }

    public virtual FileState FileState { get; set; } = null!;

    public virtual FileType FileType { get; set; } = null!;

    public virtual ICollection<Metro2> Metro2s { get; set; } = new List<Metro2>();

    public virtual ReportingPeriod ReportingPeriodFileNavigation { get; set; } = null!;

    public virtual ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();
}
