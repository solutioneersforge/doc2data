import { Routes } from '@angular/router';
import { HomeComponent } from './shared/header/home.component';
import { ContactComponent } from './shared/header/contact.component';
import { AboutComponent } from './shared/header/about.component';
import { ReceiptProcessComponent } from './receipts/receipt-process/receipt-process.component';
import { PagenotfoundComponent } from './shared/header/pagenotfound/pagenotfound.component';
import { LoginComponent } from './authentication/login.component';
import { RegistartionComponent } from './authentication/registartion.component';
import { ReceiptProcessDashboardComponent } from './receipts/receipt-process-dashboard.component';
import { ReceiptVerificationComponent } from './receipts/receipt-verification.component';

export const routes: Routes = [
    {path: '', redirectTo: '/home', pathMatch: 'full'},
    {path: 'home', component: HomeComponent},
    {path: 'contact', component: ContactComponent},
    {path: 'about', component: AboutComponent},
    {path: 'scanner', component: ReceiptProcessComponent},
    {path: 'dashboard', component: ReceiptProcessDashboardComponent},
    {path: 'receiptverification/:id', component: ReceiptVerificationComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegistartionComponent},
    {path: '**', component:PagenotfoundComponent}
];
