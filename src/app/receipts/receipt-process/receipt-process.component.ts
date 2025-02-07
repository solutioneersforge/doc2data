import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ReceiptDetails } from '../../interfaces/receipt-details.model';
import { ReceiptDetailsService } from '../../services/receipt-details.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ExpenseCategoriesDTO } from '../../interfaces/expense-categories-dto';

@Component({
  selector: 'app-receipt-process',
  imports: [ReactiveFormsModule ],
  templateUrl: './receipt-process.component.html',
  styleUrl: './receipt-process.component.css'
})
export class ReceiptProcessComponent implements OnInit {
  ngOnInit(): void {
    this.getExpenseSubCategoriesDTO();
  }
  imageUrl: string | ArrayBuffer | null = null;
  previewImage: string | ArrayBuffer | null = null;
  isLoading: boolean = true;
  loaderStarted : boolean = false;
    receiptDetailsService = inject(ReceiptDetailsService);

    domSanitizer = inject(DomSanitizer);
    expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];

    selectedFile: File | null = null;
    previewPdf: SafeResourceUrl | null = null;
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
  
  onFileChange(event: any): void {
    const file = event.target.files[0];
    
    this.selectedFile = event.target.files[0];
    debugger;
    if (file) {
      this.receiptFormGroup.reset();
      this.receiptDetails.receiptItems = [];
      this.imageUrl = null;
      this.isLoading = false;
      const reader = new FileReader();
      if (file.type.startsWith('image/')) {
        reader.onload = () => {
          this.previewImage = reader.result;
          this.previewPdf = null; 
        };
        reader.readAsDataURL(file);
      } else if (file.type === 'application/pdf') {
        // PDF Preview
        const fileUrl = URL.createObjectURL(file);
        this.previewPdf = this.domSanitizer.bypassSecurityTrustResourceUrl(fileUrl);
        this.previewImage = null;  // Reset image preview
      }
    }
  }

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

    processReceipt() {
        this.isLoading = true;
        this.loaderStarted = true;
        this.receiptDetailsService
              .getReceiptItemsDetails(this.selectedFile)
              .subscribe({next: (data : any) => {
                console.log(data)
                if(data.isSuccess == true)
                {
                  this.receiptDetails = data.parseData
                  this.receiptFormGroup.setValue({
                    vendorAddress : this.receiptDetails.vendorAddress,
                    customerAddress: this.receiptDetails.customerAddress,
                    customerName: this.receiptDetails.customerName,
                    customerPhoneNumber: this.receiptDetails.customerPhone,
                    invoiceDate: this.receiptDetails.invoiceDate,
                    invoiceNumber: this.receiptDetails.invoiceNumber,
                    itemCount: this.receiptDetails.itemsCount,
                    paymentType: this.receiptDetails.paymentTerms,
                    subTotal: this.receiptDetails.subtotal,
                    taxAmount: this.receiptDetails.tax,
                    total : this.receiptDetails.total,
                    vendorEmail: this.receiptDetails.vendorEmailAddress,
                    vendorName: this.receiptDetails.vendorName,
                    vendorPhone: this.receiptDetails.vendorPhone
                  });
              } else{
              }},
              error: (data) => console.log(data),
               complete: () => {this.isLoading = false; this.loaderStarted = false;}});
      }

      getExpenseSubCategoriesDTO(){
        this.receiptDetailsService.getExpenseSubCategoriesDTO().subscribe(data => {
          this.expenseCategoriesDTO = data.data;
          console.log("sdfsdf")
          console.log(this.expenseCategoriesDTO)
        });
      }
}
