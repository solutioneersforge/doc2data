import { Component, inject, OnInit } from '@angular/core';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ReceiptDashboardDTO } from '../interfaces/receipt-dashboard-dto';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink} from '@angular/router';

@Component({
  selector: 'app-receipt-process-dashboard',
  imports: [DatePipe, CurrencyPipe, RouterLink],
  templateUrl: './receipt-process-dashboard.component.html',
  styleUrl: './receipt-process-dashboard.component.css'
})
export class ReceiptProcessDashboardComponent implements OnInit {
  receiptDetailsService = inject(ReceiptDetailsService);
  receiptDashboardDTO: ReceiptDashboardDTO[] = [];
  isLoading : boolean = true;
  constructor() { }
  ngOnInit(): void {
    this.getFunctionAppReceiptDashboard();
  }

  getFunctionAppReceiptDashboard(){
    this.receiptDetailsService
            .getFunctionAppReceiptDashboard()
            .subscribe({
              next: (data : any) => {
                this.isLoading = true;
                this.receiptDashboardDTO = data.data.result
              },
              error: (error) => console.error(error),
              complete: () => this.isLoading = false
            }
          );
  }
}
