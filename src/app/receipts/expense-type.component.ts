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
    this.counterIndex = 1;
    this.getExpenseSubCategoriesDTO();
  }
  counterIndex : number = 1;
  category: string = '';
  subcategory: string = '';
  editIndex: number | null = null;
  receiptDetailsService = inject(ReceiptDetailsService);
  showCategoryList = false;
  showSubcategoryList = false;
  expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];
  expenseSubCategoriesDTO: ExpenseSubCategoriesDTO[] = [];
  getSequence(){
    return this.counterIndex++;
  }
 

  getExpenseSubCategoriesDTO() {
    this.receiptDetailsService
      .getExpenseSubCategoriesDTO()
      .subscribe((data) => {
        this.expenseCategoriesDTO = data.data;
        this.counterIndex = 1;
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

  selectSubcategory(sub: string) {
    this.subcategory = sub;
    this.showSubcategoryList = false;
  }

  hideDropdown(type: string) {
    setTimeout(() => {
      if (type === 'category') this.showCategoryList = false;
      if (type === 'subcategory') this.showSubcategoryList = false;
    }, 200);
  }
}
