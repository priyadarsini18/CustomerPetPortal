import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  isSidebarOpen = true;

  constructor(
    public router: Router
  ) { }

  toggleSidebar(): void {

    this.isSidebarOpen =
      !this.isSidebarOpen;

  }

  navigate(route: string): void {

    this.router.navigate([
      route
    ]);

  }

  logout(): void {

    localStorage.clear();

    this.router.navigate([
      '/login'
    ]);

  }

}
