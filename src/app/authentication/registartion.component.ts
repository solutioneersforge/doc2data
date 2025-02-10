import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registartion',
  imports: [FormsModule],
  templateUrl: './registartion.component.html',
  styleUrl: './registartion.component.css'
})
export class RegistartionComponent {
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  captchaInput: string = '';
  
  // Simple captcha answer
  captchaAnswer: number = 8; // 5 + 3

  onSubmit() {
    // Password confirmation check
    if (this.password !== this.confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    // Captcha check
    if (parseInt(this.captchaInput) !== this.captchaAnswer) {
      alert("Captcha is incorrect!");
      return;
    }

    // You can now submit the form data to your backend
    console.log('Form submitted successfully!', {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
      captcha: this.captchaInput,
    });
  }
}
