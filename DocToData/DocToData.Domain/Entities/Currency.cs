using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class Currency
{
    public int CurrenctId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Symbol { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
