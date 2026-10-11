import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  email = '';
  password = '';

  showPassword = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  login(): void {

    if (!this.email || !this.password) {

      alert('Please enter email and password');
      return;

    }

    this.authService
      .login(this.email, this.password)
      .subscribe({

        next: (response: any) => {

          localStorage.setItem(
            'customerId',
            response.customerId.toString()
          );

          localStorage.setItem(
            'firstName',
            response.firstName || ''
          );

          localStorage.setItem(
            'lastName',
            response.lastName || ''
          );

          localStorage.setItem(
            'email',
            response.email || ''
          );

          localStorage.setItem(
            'phoneNumber',
            response.phone || ''
          );

          this.router.navigate([
            '/dashboard'
          ]);

        },

        error: () => {

          alert(
            'Invalid Email or Password'
          );

        }

      });

  }

  goToRegister(): void {

    this.router.navigate([
      '/register'
    ]);

  }

}