import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar {

  constructor(
    private router: Router
  ) { }

  logout(): void {

    localStorage.removeItem('customerId');

    localStorage.removeItem('token');

    localStorage.clear();

    this.router.navigate([
      '/login'
    ]);

  }

}