using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class ErrorCategory
{
    public long ErrorCategoryId { get; set; }

    public string ErrorCategory1 { get; set; } = null!;

    public virtual ICollection<RejectCategory> RejectCategories { get; set; } = new List<RejectCategory>();
}
