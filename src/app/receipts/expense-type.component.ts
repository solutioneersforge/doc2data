import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormsModule, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ExpenseCategoriesDTO } from '../interfaces/expense-categories-dto';
import { ExpenseSubCategoriesDTO } from '../interfaces/expense-sub-categories-dto';
import { ExpenseTypeDTO } from '../interfaces/expense-type-dto';
import { CommonModule } from '@angular/common';
import { UnitOfMeasuresDTO } from '../interfaces/unit-of-measures-dto';

@Component({
  selector: 'app-expense-type',
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './expense-type.component.html',
  styleUrl: './expense-type.component.css',
})
export class ExpenseTypeComponent implements OnInit {
  ngOnInit(): void {
    this.getExpenseSubCategoriesDTO();
  }
  isNewExpense : boolean = true;
  isLoading : boolean = true;
  category: string = '';
  subcategory: string = '';
  editIndex: number | null = null;
  receiptDetailsService = inject(ReceiptDetailsService);
  showCategoryList = false;
  showSubcategoryList = false;
  expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];
 

  expenseSubCategoriesDTO: ExpenseSubCategoriesDTO[] = [];
  tooltip!: bootstrap.Tooltip;
  headerMessage : string = 'New Account Type'

  expenseTypeDTO: ExpenseTypeDTO = {
    categoryName: '',
    subCategoryName: ''
  };

  addNewExpense(){
    this.isNewExpense = true;
    this.headerMessage = "New Account Type"
  }

  addNewSubexpense(){
    this.isNewExpense = false;
    this.headerMessage = "New Subaccount Type"
  }

  formExpenseGroup = new FormGroup(
    {
      expenseType: new FormControl('', [Validators.required]),
      subExpenseType: new FormControl('',[Validators.required]),
      expenseTypeId: new FormControl()
    }
  )

  get f(){
    return this.formExpenseGroup.controls;
  }

  showTooltip() {
    if (this.tooltip) {
      this.tooltip.show();
    }
  }
 
  getErrorMessage(control: any): string {
    if (control.errors?.['required']) {
      return 'Account Type is required';
    }
    return '';
  }

  getErrorMessageSub(control: any): string {
    if (control.errors?.['required']) {
      return 'Subaccount Type is required';
    }
    return '';
  }

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

  saveExpense(){
    if(!this.formExpenseGroup.valid){
      return;
    }
    if(this.isNewExpense){
      this.expenseTypeDTO.categoryName = this.formExpenseGroup.value.expenseType ?? '';
    }
    else{
      this.expenseTypeDTO.categoryId = this.formExpenseGroup.value.expenseTypeId;
    }
    
    this.expenseTypeDTO.subCategoryName = this.formExpenseGroup.value.subExpenseType ?? '';
    this.receiptDetailsService
              .postFunctionAppExpenseType(this.expenseTypeDTO)
              .subscribe(data => {
                if(data.isSuccess){
                  this.getExpenseSubCategoriesDTO();
                  this.formExpenseGroup.reset();
                }
              });
  }

}
