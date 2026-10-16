import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../core/services/customer.service';
import { PolicyService }
from '../../core/services/policy.service';
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
  hasActivePolicies = false;

  constructor(
  private customerService: CustomerService,
  private policyService: PolicyService,
  private cdr: ChangeDetectorRef
) { }

  ngOnInit(): void {

    console.log(
      'Customer Id:',
      this.customerId
    );

    if (this.customerId > 0) {

  this.loadProfile();

  this.checkActivePolicies();

} else {


      console.error(
        'Customer ID not found'
      );

      this.isLoading = false;

    }

  }
  allowOnlyLetters(
  event: KeyboardEvent
): void {

  const charCode =
    event.which
      ? event.which
      : event.keyCode;

  const char =
    String.fromCharCode(charCode);

  if (
    !/[A-Za-z ]/.test(char)
  ) {
    event.preventDefault();
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

  this.customerService
    .updateCustomer(
      this.customerId,
      this.customer
    )
    .subscribe({

      next: (response: any) => {

        console.log(
          'Profile Updated:',
          response
        );

        alert(
          'Profile Updated Successfully'
        );

      },

      error: (err: any) => {

        console.error(
          'Profile Update Error:',
          err
        );

        alert(
          'Profile Update Failed'
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
  deleteAccount(): void {

  if (
    !confirm(
      'Are you sure you want to delete your account?'
    )
  ) {
    return;
  }

  this.customerService
    .deleteCustomer(this.customerId)
    .subscribe({
      next: () => {

        alert(
          'Account deleted successfully.'
        );

        localStorage.clear();

      },

      error: (err: any) => {

        alert(
  err.error ||
  'Customer has active policies and cannot be deleted.'
);

      }
    });

}
checkActivePolicies(): void {

  this.policyService
    .getPolicies(this.customerId)
    .subscribe({
      next: (policies: any[]) => {

        this.hasActivePolicies =
          policies.some(
            p => p.status === 'Active'
          );

      },
      error: (err:any) => {
        console.error(err);
      }
    });

}


}
