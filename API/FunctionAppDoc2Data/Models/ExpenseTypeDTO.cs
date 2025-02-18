using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class ExpenseTypeDTO
{
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int? SubcategoryId { get; set; }
    public string SubCategoryName { get; set; }

}
