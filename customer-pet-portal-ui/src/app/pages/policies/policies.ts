import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';

import { PolicyService }
from '../../core/services/policy.service';

@Component({
  selector: 'app-policies',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './policies.html',
  styleUrls: ['./policies.css']
})
export class Policies implements OnInit {

  policies: any[] = [];

  selectedPolicy: any = null;

  isLoading = true;

  customerId = Number(
    localStorage.getItem('customerId')
  );

  constructor(
    private policyService: PolicyService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {

    console.log(
      'CustomerId:',
      this.customerId
    );

    this.loadPolicies();

  }

  loadPolicies(): void {

    console.log(
      'Calling API'
    );

    this.isLoading = true;

    this.policyService
      .getPolicies(
        this.customerId
      )
      .subscribe({

        next: (data: any[]) => {

          console.log(
            'Policies loaded:',
            data
          );

          this.policies = [...data];

          this.isLoading = false;

          this.cdr.detectChanges();

        },

        error: (err: any) => {

          console.error(
            'Load Policies Error',
            err
          );

          this.isLoading = false;

          this.cdr.detectChanges();

        }

      });

  }

  deactivatePolicy(
    policyId: number
  ): void {

    this.policyService
      .deactivatePolicy(
        this.customerId,
        policyId
      )
      .subscribe({

        next: () => {

          alert(
            'Policy Deactivated Successfully'
          );

          this.closePolicy();

          this.loadPolicies();

        },

        error: (err: any) => {

          console.error(
            'Deactivate Policy Error',
            err
          );

        }

      });

  }

  deletePolicy(
    policyId: number
  ): void {

    if (
      !confirm(
        'Are you sure you want to delete this policy?'
      )
    ) {
      return;
    }

    this.policyService
      .deletePolicy(
        this.customerId,
        policyId
      )
      .subscribe({

        next: () => {

          alert(
            'Policy Deleted Successfully'
          );

          this.closePolicy();

          this.loadPolicies();

        },

        error: (err: any) => {

          console.error(
            'Delete Policy Error',
            err
          );

          alert(
            err.error?.message ||
            'Unable to delete policy.'
          );

        }

      });

  }

  viewPolicy(
    policy: any
  ): void {

    this.selectedPolicy =
      policy;

  }

  closePolicy(): void {

    this.selectedPolicy =
      null;

  }

}