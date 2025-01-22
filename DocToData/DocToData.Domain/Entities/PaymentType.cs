using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class PaymentType
{
    public int PaymentTypeId { get; set; }

    public string PaymentType1 { get; set; } = null!;

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
