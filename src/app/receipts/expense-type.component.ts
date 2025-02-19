import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ExpenseCategoriesDTO } from '../interfaces/expense-categories-dto';
import { ExpenseSubCategoriesDTO } from '../interfaces/expense-sub-categories-dto';

@Component({
  selector: 'app-expense-type',
  imports: [FormsModule],
  templateUrl: './expense-type.component.html',
  styleUrl: './expense-type.component.css',
})
export class ExpenseTypeComponent implements OnInit {
  ngOnInit(): void {
    this.getExpenseSubCategoriesDTO();
  }
  isLoading : boolean = true;
  category: string = '';
  subcategory: string = '';
  editIndex: number | null = null;
  receiptDetailsService = inject(ReceiptDetailsService);
  showCategoryList = false;
  showSubcategoryList = false;
  expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];
  expenseSubCategoriesDTO: ExpenseSubCategoriesDTO[] = [];
 

  getExpenseSubCategoriesDTO() {
    this.receiptDetailsService
      .getExpenseSubCategoriesDTO()
      .subscribe({next: (data) => {
        this.expenseCategoriesDTO = data.data;
      },
      error: (error) => console.log(error),
      complete : () => this.isLoading = false
    });
  }

  selectCategory(categoryName: string, categoryId: number) {
    this.category = categoryName;
    this.showCategoryList = false;
    this.expenseSubCategoriesDTO = [];
    this.expenseSubCategoriesDTO = this.expenseCategoriesDTO
      .filter((m) => m.categoryId === categoryId)
      .map((d) => d.expenseSubCategoriesDTOs)
      .flat();
      this.subcategory = '';
  }

}
