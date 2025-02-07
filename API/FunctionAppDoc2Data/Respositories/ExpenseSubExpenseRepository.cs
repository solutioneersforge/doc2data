
using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.Respositories;
public class ExpenseSubExpenseRepository : IExpenseSubExpenseRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;

    public ExpenseSubExpenseRepository(DocToDataDBContext docToDataDBContext)
    {
        _docToDataDBContext = docToDataDBContext;
    }
    public IEnumerable<ExpenseCategoriesDTO> GetExpenseSubExpenseCategory()
    {
        IEnumerable<ExpenseCategory> expenseCategories = _docToDataDBContext.ExpenseCategories.Include(m => m.ExpenseSubCategories);
        return expenseCategories.MapToExpenseCategoriesDTO();
    }
}
