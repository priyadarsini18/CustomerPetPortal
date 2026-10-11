import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CustomerService } from '../../core/services/customer.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile implements OnInit {

  customer = {
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    communicationPreference: 'Email'
  };

  customerId = Number(
    localStorage.getItem('customerId')
  );

  isLoading = true;

  constructor(
    private customerService: CustomerService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {

    console.log(
      'Customer Id:',
      this.customerId
    );

    if (this.customerId > 0) {

      this.loadProfile();

    } else {

      console.error(
        'Customer ID not found'
      );

      this.isLoading = false;

    }

  }

  loadProfile(): void {

    this.customerService
      .getCustomer(this.customerId)
      .subscribe({

        next: (data: any) => {

          console.log(
            'Customer Profile:',
            data
          );

          this.customer = {
            firstName:
              data.firstName || '',
            lastName:
              data.lastName || '',
            email:
              data.email || '',
            phone:
              data.phone || '',
            communicationPreference:
              data.communicationPreference || 'Email'
          };

          this.isLoading = false;

          this.cdr.detectChanges();

        },

        error: (err: any) => {

          console.error(
            'Profile Load Error:',
            err
          );

          this.isLoading = false;

        }

      });

  }

  saveProfile(): void {

    console.log(
      'Updated Customer:',
      this.customer
    );

    alert(
      'Profile Updated Successfully'
    );

  }

}