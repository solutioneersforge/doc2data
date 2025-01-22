using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class Merchant
{
    public int MerchantId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int CountryId { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool IsActive { get; set; }

    public string? Website { get; set; }

    public string? CompanyRegNo { get; set; }

    public string? TaxCompanyRegNo { get; set; }
}
