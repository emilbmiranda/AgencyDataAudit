using System;
using System.Collections.Generic;

namespace AgencyDataAuditAPI.Models;

public partial class ErrorCategory
{
    public long ErrorCategoryId { get; set; }

    public string ErrorCategoryName { get; set; } = null!;

    public virtual ICollection<Error> Errors { get; set; } = new List<Error>();

    public virtual ICollection<RejectCategory> RejectCategories { get; set; } = new List<RejectCategory>();
}
