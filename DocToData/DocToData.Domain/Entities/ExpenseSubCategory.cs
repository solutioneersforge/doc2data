using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class ExpenseSubCategory
{
    public int SubCategoryId { get; set; }

    public int CategoryId { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public bool IsActive { get; set; }
}
