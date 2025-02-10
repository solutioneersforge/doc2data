import { Component, inject } from '@angular/core';
import { SafeResourceUrl } from '@angular/platform-browser';
import { ReceiptDetails } from '../interfaces/receipt-details.model';
import { FormControl, FormGroup } from '@angular/forms';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ExpenseCategoriesDTO } from '../interfaces/expense-categories-dto';

@Component({
  selector: 'app-receipt-verification',
  imports: [],
  templateUrl: './receipt-verification.component.html',
  styleUrl: './receipt-verification.component.css'
})
export class ReceiptVerificationComponent {
  imageUrl: string | ArrayBuffer | null = null;
  expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];
  receiptDetailsService = inject(ReceiptDetailsService);
  previewImage: string | ArrayBuffer | null = null;
  previewPdf: SafeResourceUrl | null = null;
  isSaveLoader: boolean = false;
  isSaveButtonEnable: boolean = false;
  
 receiptDetails : ReceiptDetails = {
    receiptItems: [],
    transactionTime: null,
    issueDate: null,
    issueTime: null,
    invoiceDate: null,
    dueDate: null,
    vendorAddress: null,
    vendorTIN: null,
    customerName: null,
    customerAddress: null,
    customerPhone: null,
    customerTIN: null,
    currency: null,
    exchangeRate: 0,
    itemsCount: 0,
    shippingFee: 0,
    additionalFee: 0,
    taxableAmount: 0,
    discount: 0,
    tip: 0,
    tax: 0,
    subtotal: 0,
    amountDue: 0,
    amountPaid: 0,
    change: 0,
    balanceDue: 0,
    paymentMethod: '',
    paymentStatus: '',
    returnPolicy: '',
    paymentReferenceNumber: '',
    paymentTerms: '',
    table: '',
    server: '',
    station: '',
    serviceAddress: '',

    deliveryInfo: '',
    barcodeQRCode: '',
    unknownField: '',
    transactionDate: new Date(),
    invoiceNumber: '',
    orderNumber: '',
    vendorName: '',
    vendorPhone: '',
    vendorEmailAddress: null,
    serviceStartDate: new Date(),
    serviceEndDate: new Date(),
    total: 0
  };

  receiptFormGroup = new FormGroup({
    vendorName : new FormControl(this.receiptDetails.vendorName,[]),
    vendorAddress : new FormControl(this.receiptDetails.vendorAddress, []),
    vendorPhone: new FormControl(this.receiptDetails.vendorPhone, []),
    vendorEmail: new FormControl(this.receiptDetails.vendorEmailAddress, []),
    invoiceNumber: new FormControl(this.receiptDetails.invoiceNumber, []),
    invoiceDate: new FormControl(this.receiptDetails.invoiceDate, []),
    customerName: new FormControl(this.receiptDetails.customerName, []),
    customerAddress: new FormControl(this.receiptDetails.customerAddress,[]),
    customerPhoneNumber : new FormControl(this.receiptDetails.customerPhone,[]),
    subTotal: new FormControl(this.receiptDetails.subtotal, []),
    taxAmount: new FormControl(this.receiptDetails.taxableAmount,[]),
    total: new FormControl(this.receiptDetails.total,[]),
    paymentType: new FormControl(this.receiptDetails.paymentMethod,[]),
    itemCount: new FormControl(this.receiptDetails.itemsCount,[])
  });

      getExpenseSubCategoriesDTO(){
        this.receiptDetailsService.getExpenseSubCategoriesDTO().subscribe(data => {
          this.expenseCategoriesDTO = data.data;
        });
      }
      
  saveReceipt(){

  }

  resetControl(){

  }
}
