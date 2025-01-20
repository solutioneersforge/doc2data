using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class StockTransaction
{
    public long TransactionId { get; set; }

    public int ItemId { get; set; }

    public int TransactionTypeId { get; set; }

    public DateTime TransactionDate { get; set; }

    public int Quantity { get; set; }

    public Guid TransactionReferenceId { get; set; }

    public string? Notes { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual TransactionType TransactionType { get; set; } = null!;
}
