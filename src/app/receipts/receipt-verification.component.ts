import { Component, inject, OnInit } from '@angular/core';
import { SafeResourceUrl } from '@angular/platform-browser';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ExpenseCategoriesDTO } from '../interfaces/expense-categories-dto';
import { ReceiptVerificationMasterDTO } from '../interfaces/receipt-verification-master-dto';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-receipt-verification',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './receipt-verification.component.html',
  styleUrl: './receipt-verification.component.css'
})
export class ReceiptVerificationComponent implements OnInit {
  imageBase64: string = '';
  isImage: boolean = true;
  constructor(private activatedRoute: ActivatedRoute, private router: Router){

  }
  ngOnInit() {
    this.activatedRoute.params.subscribe(data => {
      this.getFunctionAppReceiptVerification(data["id"]);
    });
    
  }

  imageUrl: string | ArrayBuffer | null = null;
  expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];
  receiptDetailsService = inject(ReceiptDetailsService);
  previewImage: string | ArrayBuffer | null = null;
  previewPdf: SafeResourceUrl | null = null;
  isSaveLoader: boolean = false;
  isSaveButtonEnable: boolean = false;
  receiptVerificationMaster: ReceiptVerificationMasterDTO = {
    receiptId: '',
    userId: '',
    vendorName: '',
    vendorAddress: '',
    vendorPhone: '',
    vendorEmail: '',
    customerName: '',
    customerAddress: '',
    customerPhone: '',
    invoiceNumber: '',
    invoiceDate: '',
    subTotal: 0,
    taxAmount: 0,
    total: 0,
    imagePath: '',
    image : '',
    originalFileName: '',
    isImage: true,
    receiptVerificationItems: []
  };
  


  receiptFormGroup = new FormGroup({
    vendorName : new FormControl(this.receiptVerificationMaster.vendorName,[]),
    vendorAddress : new FormControl(this.receiptVerificationMaster.vendorAddress, []),
    vendorPhone: new FormControl(this.receiptVerificationMaster.vendorPhone, []),
    vendorEmail: new FormControl(this.receiptVerificationMaster.vendorEmail, []),
    invoiceNumber: new FormControl(this.receiptVerificationMaster.invoiceNumber, []),
    invoiceDate: new FormControl(this.receiptVerificationMaster.invoiceDate, []),
    customerName: new FormControl(this.receiptVerificationMaster.customerName, []),
    customerAddress: new FormControl(this.receiptVerificationMaster.customerAddress,[]),
    customerPhoneNumber : new FormControl(this.receiptVerificationMaster.customerPhone,[]),
    subTotal: new FormControl(this.receiptVerificationMaster.subTotal, []),
    taxAmount: new FormControl(this.receiptVerificationMaster.taxAmount,[]),
    total: new FormControl(this.receiptVerificationMaster.total,[]),
    imageBase64 : new FormControl(this.receiptVerificationMaster.image,[])
  });

    getExpenseSubCategoriesDTO(){
      this.receiptDetailsService.getExpenseSubCategoriesDTO().subscribe(data => {
        this.expenseCategoriesDTO = data.data;
      });
    }


    getFunctionAppReceiptVerification(receiptId: string){
      this.receiptDetailsService.getFunctionAppReceiptVerification(receiptId).subscribe(data => {
        this.receiptVerificationMaster = data.data;
        this.isImage = this.receiptVerificationMaster.isImage;
        this.imageBase64 = this.receiptVerificationMaster.image;
        this.receiptFormGroup.setValue({
          vendorAddress : this.receiptVerificationMaster.vendorAddress,
          customerAddress: this.receiptVerificationMaster.customerAddress,
          customerName: this.receiptVerificationMaster.customerName,
          customerPhoneNumber: this.receiptVerificationMaster.customerPhone,
          invoiceDate: this.receiptVerificationMaster.invoiceDate,
          invoiceNumber: this.receiptVerificationMaster.invoiceNumber,
          subTotal: this.receiptVerificationMaster.subTotal,
          taxAmount: this.receiptVerificationMaster.taxAmount,
          total : this.receiptVerificationMaster.total,
          vendorEmail: this.receiptVerificationMaster.vendorEmail,
          vendorName: this.receiptVerificationMaster.vendorName,
          vendorPhone: this.receiptVerificationMaster.vendorPhone,
          imageBase64: this.receiptVerificationMaster.image
        });
      });
    }

    displayDataToFormControl(){
      
    }
      
  saveReceipt(){

  }

  resetControl(){
    this.router.navigate(['/dashboard']);
  }
}
