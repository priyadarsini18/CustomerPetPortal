import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-policies',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './policies.html',
  styleUrl: './policies.css'
})
export class Policies implements OnInit {

  policies: any[] = [];

  selectedPolicy: any = null;

  isLoading = true;

  constructor() { }

  ngOnInit(): void {

    this.loadPolicies();

  }

  loadPolicies(): void {

    this.policies = [

      {
        policyNumber: 'POL001',
        coverage: 'Gold Plan',
        premium: 1200,
        status: 'Active',
        effectiveDate: '2026-01-01',
        expiryDate: '2026-12-31'
      },

      {
        policyNumber: 'POL002',
        coverage: 'Silver Plan',
        premium: 900,
        status: 'Active',
        effectiveDate: '2026-02-01',
        expiryDate: '2027-01-31'
      }

    ];

    this.isLoading = false;

  }

  viewPolicy(
    policy: any
  ): void {

    this.selectedPolicy = policy;

  }

  closePolicy(): void {

    this.selectedPolicy = null;

  }

}