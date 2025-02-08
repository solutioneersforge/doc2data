using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class ExpenseCategory
    {
        public ExpenseCategory()
        {
            ExpenseSubCategories = new HashSet<ExpenseSubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ExpenseSubCategory> ExpenseSubCategories { get; set; }
    }
}
