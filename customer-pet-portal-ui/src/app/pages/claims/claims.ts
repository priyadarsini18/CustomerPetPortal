import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';

import { ClaimService }
from '../../core/services/claim.service';

@Component({
  selector: 'app-claims',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './claims.html',
  styleUrls: ['./claims.css']
})
export class Claims implements OnInit {

  claims: any[] = [];

  selectedClaim: any = null;

  isLoading = true;

  customerId = Number(
    localStorage.getItem('customerId')
  );

  constructor(
    private claimService: ClaimService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {

    console.log(
      'Customer Id:',
      this.customerId
    );

    this.loadClaims();

  }

  loadClaims(): void {

    this.isLoading = true;

    this.claimService
      .getClaims(
        this.customerId
      )
      .subscribe({

        next: (data: any[]) => {

          console.log(
            'Claims:',
            data
          );

          this.claims = [...data];

          this.isLoading = false;

          this.cdr.detectChanges();

        },

        error: (err: any) => {

          console.error(
            'Load Claims Error',
            err
          );

          this.isLoading = false;

        }

      });

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