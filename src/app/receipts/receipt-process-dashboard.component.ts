import { Component, inject, OnInit } from '@angular/core';
import { ReceiptDetailsService } from '../services/receipt-details.service';
import { ReceiptDashboardDTO } from '../interfaces/receipt-dashboard-dto';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-receipt-process-dashboard',
  imports: [DatePipe, CurrencyPipe, RouterLink, RouterOutlet],
  templateUrl: './receipt-process-dashboard.component.html',
  styleUrl: './receipt-process-dashboard.component.css'
})
export class ReceiptProcessDashboardComponent implements OnInit {
  receiptDetailsService = inject(ReceiptDetailsService);
  receiptDashboardDTO: ReceiptDashboardDTO[] = [];
  constructor() { }
  ngOnInit(): void {
    this.getFunctionAppReceiptDashboard();
  }

  getFunctionAppReceiptDashboard(){
    this.receiptDetailsService
            .getFunctionAppReceiptDashboard()
            .subscribe({
              next: (data : any) => {
                this.receiptDashboardDTO = data.data.result
              }
            });
  }
}
