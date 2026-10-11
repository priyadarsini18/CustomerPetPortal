import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-claims',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './claims.html',
  styleUrl: './claims.css'
})
export class Claims implements OnInit {

  claims: any[] = [];

  selectedClaim: any = null;

  isLoading = true;

  constructor() { }

  ngOnInit(): void {

    this.loadClaims();

  }

  loadClaims(): void {

    this.claims = [

      {
        claimNumber: 'CLM001',
        treatmentDate: '2026-10-08',
        claimedAmount: 5000,
        approvedAmount: 4500,
        status: 'Approved'
      },

      {
        claimNumber: 'CLM002',
        treatmentDate: '2026-10-09',
        claimedAmount: 3000,
        approvedAmount: 3000,
        status: 'Approved'
      }

    ];

    this.isLoading = false;

  }

  viewClaim(
    claim: any
  ): void {

    this.selectedClaim = claim;

  }

  closeClaim(): void {

    this.selectedClaim = null;

  }

}
