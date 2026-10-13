import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  customer = {
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    communicationPreference: 'Email',
    password: '',
    addressLine1: '',
    city: '',
    state: '',
    zipCode: '',
    confirmPassword: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  register(): void {

    if (
      this.customer.password !==
      this.customer.confirmPassword
    ) {

      alert(
        'Password and Confirm Password do not match'
      );

      return;
    }

    this.authService
      .register(this.customer)
      .subscribe({
        next: (response: any) => {

          alert(
            'Registration Successful'
          );

          this.router.navigate(
            ['/login']
          );
        },

        error: (err: any) => {

          console.error(
            'Registration Error',
            err
          );

        }
      });
  }

  allowOnlyNumbers(
    event: KeyboardEvent
  ): void {

    const charCode =
      event.which
        ? event.which
        : event.keyCode;

    if (
      charCode < 48 ||
      charCode > 57
    ) {
      event.preventDefault();
    }

  }

}
