using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ExpenseTypeRepository : IExpenseTypeRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    private readonly ILogger<ExpenseTypeRepository> _logger;

    public ExpenseTypeRepository(DocToDataDBContext docToDataDBContext, ILogger<ExpenseTypeRepository> logger)
    {
        _docToDataDBContext = docToDataDBContext;
        _logger = logger;
    }
    public int UpsertExpenseCategoryAndSubcategory(ExpenseTypeDTO expenseType)
    {
        if (expenseType == null) throw new ArgumentNullException(nameof(expenseType));
        if (!expenseType.CategoryId.HasValue || expenseType.CategoryId == 0)
        {
            if (_docToDataDBContext.ExpenseCategories.Any(m => m.CategoryName == expenseType.CategoryName))
            {
                return 0;
            }
        }

        using var transaction = _docToDataDBContext.Database.BeginTransaction();
        try
        {
            int result = 0;

            // Ensure Category exists or create it
            var category = GetOrCreateCategory(expenseType);

            // Ensure Subcategory exists or create/update it
            if (!string.IsNullOrEmpty(expenseType.SubCategoryName))
            {
                UpsertSubCategory(expenseType, category.CategoryId);
            }

            result = _docToDataDBContext.SaveChanges();
            transaction.Commit();
            return result;
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError($"ArgumentNullException: {ex.Message}", ex);
            throw;
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"KeyNotFoundException: {ex.Message}", ex);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error: {ex.Message}", ex);
            return 0;
        }
    }


    private ExpenseCategory GetOrCreateCategory(ExpenseTypeDTO expenseType)
    {
        var category = expenseType.CategoryId.HasValue && expenseType.CategoryId > 0
            ? _docToDataDBContext.ExpenseCategories.Find(expenseType.CategoryId)
            : null;

        if (category == null)
        {
            category = new ExpenseCategory { CategoryName = expenseType.CategoryName };
            _docToDataDBContext.ExpenseCategories.Add(category);
            _docToDataDBContext.SaveChanges(); // Assigns CategoryId
        }
        else if (!string.IsNullOrEmpty(expenseType.CategoryName))
        {
            category.CategoryName = expenseType.CategoryName;
        }

        return category;
    }

    private void UpsertSubCategory(ExpenseTypeDTO expenseType, int categoryId)
    {
        var subCategory = expenseType.SubcategoryId.HasValue && expenseType.SubcategoryId > 0
            ? _docToDataDBContext.ExpenseSubCategories.Find(expenseType.SubcategoryId)
            : null;

        if (subCategory == null)
        {
            _docToDataDBContext.ExpenseSubCategories.Add(new ExpenseSubCategory
            {
                CategoryId = categoryId,
                SubCategoryName = expenseType.SubCategoryName
            });
        }
        else
        {
            subCategory.SubCategoryName = expenseType.SubCategoryName;
        }
    }


}
