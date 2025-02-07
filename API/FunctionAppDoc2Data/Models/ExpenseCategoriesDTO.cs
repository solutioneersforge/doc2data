using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class ExpenseCategoriesDTO
{
    public  int CategoryId { get; set; }
    public  string CategoryName { get; set; }
    public  bool IsActive { get; set; }
    public IEnumerable<ExpenseSubCategoriesDTO> ExpenseSubCategoriesDTOs { get; set; }
}
