import { Component, inject } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReceiptDetailsService } from './services/receipt-details.service';
import { ReceiptDetails } from './interfaces/receipt-details.model';
import { MatGridListModule } from '@angular/material/grid-list';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatSnackBar} from '@angular/material/snack-bar';
import { exhaustMap, forkJoin } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [ MatTableModule,
    MatCardModule,
      MatButtonModule,
      MatIconModule,
      MatFormFieldModule,
      MatInputModule,
      MatGridListModule,
      MatProgressSpinnerModule,
      ReactiveFormsModule,
      MatProgressBarModule,
    ],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent {
  isLoading: boolean = false;
  isProcessButtonDisable : boolean = false;

  private _snackBar = inject(MatSnackBar);
  selectedImage: string | ArrayBuffer | null = null;
  receiptDetailsService = inject(ReceiptDetailsService);
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
  dataSource = new MatTableDataSource(this.receiptDetails.receiptItems);  

  displayedColumns: string[] = ['description', 'quantity', 'unitPrice', 'discount' , 'totalPrice'];

  selectedFile: File | null = null;
  previewUrl: string | null = null;
  isProcessing = false;
  durationInSeconds = 5;

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

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    if (this.selectedFile) {
      const reader = new FileReader();
      reader.onload = (e: any) =>  { this.previewUrl = e.target.result;  this.selectedImage = reader.result;}
      reader.readAsDataURL(this.selectedFile);
    }
  }

  processReceipt() {
    this.isProcessing = true;
    this.isLoading = true;
    this.receiptDetailsService
          .getReceiptItemsDetails(this.selectedFile)
          .pipe(exhaustMap((data) => data))
          .subscribe({next: (data : any) => {
            if(data.isSuccess == true)
            {
              this._snackBar.open("File sucessfully processed.", "Success");
              this.receiptDetails = data.parseData
              this.dataSource = new MatTableDataSource(this.receiptDetails?.receiptItems)
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
            this._snackBar.open("Due to some technical issue unable to process the file.", "Error");
          }},
          error: (data) => console.log(data),
           complete: () => {this.isLoading = false;}});
  }
}
