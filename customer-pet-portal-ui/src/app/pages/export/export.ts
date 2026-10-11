import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExportService }
from '../../core/services/export.service';

@Component({
  selector: 'app-export',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './export.html',
  styleUrl: './export.css'
})
export class ExportPage {

  constructor(
    private exportService: ExportService
  ) { }

  exportWorkbook(): void {

    this.exportService
      .exportWorkbook()
      .subscribe({

        next: (file: Blob) => {

          const url =
            window.URL.createObjectURL(file);

          const link =
            document.createElement('a');

          link.href = url;

          link.download =
            'Customers.xlsx';

          document.body.appendChild(link);

          link.click();

          document.body.removeChild(link);

          window.URL.revokeObjectURL(url);

        },

        error: (err) => {

          console.error(err);

          alert(
            'Export failed'
          );

        }

      });

  }

}