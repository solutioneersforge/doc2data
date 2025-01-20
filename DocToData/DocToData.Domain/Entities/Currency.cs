using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class Currency
{
    public int CurrenctId { get; set; }

    public string CurrencyName { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public string? CurrencySymbol { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
