namespace DocToData.Domain.DTO.Common;

public class ExpenseCategoryDTO
{
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public required string SubCategoryName { get; set; }
    public bool IsActive { get; set; }
}

