using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class ExpenseCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool IsActive { get; set; }
}
