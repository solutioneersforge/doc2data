using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class ExpenseSubCategoriesDTO
{
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public  string SubCategoryName { get; set; }
    public bool IsActive { get; set; }
}
