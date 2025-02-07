import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { ReceiptDetails } from '../interfaces/receipt-details.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReceiptDetailsService {
  baseAddress : string = environment.apiUrl; 
  constructor(private httpClient : HttpClient) { }
  getReceiptItemsDetails(file: any): Observable<any>{
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.post<any>(`${this.baseAddress}api/FunctionDoc2Data`, formData);
  }

  getExpenseSubCategoriesDTO() : Observable<any>{
    return this.httpClient.get<any>(`${this.baseAddress}api/FunctionAppCategoryExpenseType`);
  }
}
