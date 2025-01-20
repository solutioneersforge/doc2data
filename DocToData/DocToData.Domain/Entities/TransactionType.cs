using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
