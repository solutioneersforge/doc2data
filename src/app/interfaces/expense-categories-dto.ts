import { ExpenseSubCategoriesDTO } from "./expense-sub-categories-dto";

export interface ExpenseCategoriesDTO {
    categoryId: number | null;
    categoryName: string | null;
    expenseSubCategoriesDTOs : ExpenseSubCategoriesDTO[];
}
