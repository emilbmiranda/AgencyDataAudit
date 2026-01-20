using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class RejectCategory
{
    public long RejectCategoryId { get; set; }

    public long ErrorCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ErrorCategory ErrorCategory { get; set; } = null!;
}
