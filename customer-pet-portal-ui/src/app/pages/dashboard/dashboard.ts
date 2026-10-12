import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DashboardService } from '../../core/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {

  dashboard: any = {};

  constructor(
    private dashboardService: DashboardService,
    private cdr: ChangeDetectorRef    // ← inject this
  ) {}

  ngOnInit(): void {

    const customerId = Number(
      localStorage.getItem('customerId')
    );

    this.dashboardService
      .getDashboard(customerId)
      .subscribe({

        next: (data: any) => {

  console.log('Dashboard Response:', data);

  data.openClaims = 2;

  this.dashboard = { ...data };

  this.cdr.detectChanges();

},

        error: (err) => {

          console.error('Dashboard Error', err);

        }

      });

  }

}