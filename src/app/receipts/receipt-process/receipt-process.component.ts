import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ReceiptDetails } from '../../interfaces/receipt-details.model';
import { ReceiptDetailsService } from '../../services/receipt-details.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ExpenseCategoriesDTO } from '../../interfaces/expense-categories-dto';
import { ExpenseSubCategoriesDTO } from '../../interfaces/expense-sub-categories-dto';
import { ReceiptMasterDTO } from '../../interfaces/receipt-master-dto';
import { ReceiptItemDTO } from '../../interfaces/receipt-item-dto';
import { NotificationService } from '../../services/notification.service';
import { NotificationComponent } from '../../shared/header/notification.component';

@Component({
  selector: 'app-receipt-process',
  imports: [ReactiveFormsModule, NotificationComponent ],
  templateUrl: './receipt-process.component.html',
  styleUrl: './receipt-process.component.css'
})
export class ReceiptProcessComponent implements OnInit {
  ngOnInit(): void {
    
    this.getExpenseSubCategoriesDTO();
  }
  imageUrl: string | ArrayBuffer | null = null;
  previewImage: string | ArrayBuffer | null = null;
  receiptItemDTOs: ReceiptItemDTO[] = [];
  isSaveButtonEnable : boolean = false;

  receiptMasterDTO: ReceiptMasterDTO = {
    userId: null,
    vendorName: null,
    vendorAddress: null,
    vendorPhone: null,
    vendorEmail: null,
    customerName: null,
    customerAddress: null,
    customerPhone: null,
    invoiceNumber: null,
    invoiceDate: null,
    subTotal: null,
    taxAmount: null,
    total: null,
    ReceiptItemDTOs: null
  };
  @ViewChild('fileInput') fileInput!: ElementRef;
  isLoading: boolean = true;
  loaderStarted : boolean = false;
  isSaveLoader: boolean = false;
    receiptDetailsService = inject(ReceiptDetailsService);
    notificationService = inject(NotificationService)

    domSanitizer = inject(DomSanitizer);
    expenseCategoriesDTO: ExpenseCategoriesDTO[] = [];
    expenseSubCategoriesDTO: ExpenseSubCategoriesDTO[] = [];

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
        const fileUrl = URL.createObjectURL(file);
        this.previewPdf = this.domSanitizer.bypassSecurityTrustResourceUrl(fileUrl);
        this.previewImage = null;  
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
                  this.isSaveButtonEnable = true;
              } else{
              }},
              error: (data) => {console.log(data); this.isSaveButtonEnable = false;},
               complete: () => {this.isLoading = false; this.loaderStarted = false; }});
      }

      getExpenseSubCategoriesDTO(){
        this.receiptDetailsService.getExpenseSubCategoriesDTO().subscribe(data => {
          this.expenseCategoriesDTO = data.data;
        });
      }

      onCategoryChange(event: any) {
       
        let filteredCategories  = this.expenseCategoriesDTO.filter(m => m.categoryId == event.target.value)
        this.expenseSubCategoriesDTO = filteredCategories.length > 0
  ? filteredCategories[0].expenseSubCategoriesDTOs 
  : [];
               
      }

      saveReceipt(){
        this.isSaveLoader = true;
        this.receiptMasterDTO.customerAddress = this.receiptFormGroup.value.customerAddress ?? null;
        this.receiptMasterDTO.customerName = this.receiptFormGroup.value.customerName ?? null;
        this.receiptMasterDTO.customerPhone = this.receiptFormGroup.value.customerPhoneNumber ?? null;
        this.receiptMasterDTO.invoiceDate = this.receiptFormGroup.value.invoiceDate ?? null;
        this.receiptMasterDTO.invoiceNumber = this.receiptFormGroup.value.invoiceNumber ?? null;
        this.receiptMasterDTO.subTotal = this.receiptFormGroup.value.subTotal ?? null;
        this.receiptMasterDTO.taxAmount = this.receiptFormGroup.value.subTotal ?? null;
        this.receiptMasterDTO.total = this.receiptFormGroup.value.subTotal ?? null;
        this.receiptMasterDTO.userId = "87C1CD94-D103-4D2B-890F-047A59FCA68D";
        this.receiptMasterDTO.vendorAddress = this.receiptFormGroup.value.vendorAddress ?? null;
        this.receiptMasterDTO.vendorEmail = this.receiptFormGroup.value.vendorEmail ?? null;
        this.receiptMasterDTO.vendorName = this.receiptFormGroup.value.vendorName ?? null;
        this.receiptMasterDTO.vendorPhone = this.receiptFormGroup.value.vendorPhone ?? null;
        
        this.receiptDetails.receiptItems.forEach(data => {
            this.receiptItemDTOs?.push({
              discount : data.discount,
              itemDescription : data.description,
              quantity : data.quantity,
              total : data.totalPrice,
              unitPrice : data.unitPrice
            })
        });
        this.receiptMasterDTO.ReceiptItemDTOs = this.receiptItemDTOs ?? null;

        this.receiptDetailsService
              .postAppCreateReceipt(this.receiptMasterDTO, this.selectedFile)
              .subscribe( {
                    next: data => {
                      console.log(data)
                if(data.isSuccess == true){
                  this.resetControl();this.notificationService.show(data.data);
                        }
                  },
                error: (error) => console.error(error),
                complete: () => this.isSaveLoader = false  
                },
                );
          }

      notification(){
        this.notificationService.show('This is a success notification!');
      }

      resetControl(){
        this.selectedFile = null;
        this.isSaveButtonEnable = false;
        this.receiptFormGroup.reset();
        this.receiptDetails.receiptItems = [];
        this.imageUrl = null;
        this.previewPdf = this.domSanitizer.bypassSecurityTrustResourceUrl('');
        this.previewImage = null;
        this.isLoading = false;this.previewImage = null;this.previewPdf = null;
        if (this.fileInput) {
          this.fileInput.nativeElement.value = ''; // Clear the input
        }
      }

      
}
