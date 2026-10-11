import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../core/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  template: `
    <h1>PAWCARE Dashboard</h1>

    <h2 style="color: green">
      Pets Count = {{ petsCount }}
    </h2>

    <h2>
      Active Policies = {{ activePolicies }}
    </h2>
  `
})
export class Dashboard implements OnInit {

  petsCount = 0;
  activePolicies = 0;

  constructor(
    private dashboardService: DashboardService
  ) {}

  ngOnInit(): void {

    this.dashboardService
      .getDashboard(1)
      .subscribe({

        next: (data: any) => {

          console.log('Received', data);

          this.petsCount = data.petsCount;

          this.activePolicies =
            data.activePolicies;

        },

        error: (err) => {

          console.error(err);

        }
      });
  }
}