using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;
}
