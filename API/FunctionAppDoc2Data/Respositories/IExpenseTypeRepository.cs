using FunctionAppDoc2Data.Models;

namespace FunctionAppDoc2Data.Respositories;
public interface IExpenseTypeRepository
{
    int UpsertExpenseCategoryAndSubcategory(ExpenseTypeDTO expenseType);
}