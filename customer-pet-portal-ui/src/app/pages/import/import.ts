import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-import',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './import.html',
  styleUrl: './import.css'
})
export class ImportPage {

  selectedFile: File | null = null;

  private apiUrl =
    'http://localhost:5278/api/excel/customers/import';

  constructor(
    private http: HttpClient
  ) { }

  onFileSelected(
    event: Event
  ): void {

    const input =
      event.target as HTMLInputElement;

    if (
      input.files &&
      input.files.length > 0
    ) {
      this.selectedFile =
        input.files[0];
    }

  }

  importWorkbook(): void {

    if (!this.selectedFile) {

      alert(
        'Please select an Excel file'
      );

      return;

    }

    const formData =
      new FormData();

    formData.append(
      'file',
      this.selectedFile
    );

    this.http
      .post(
        this.apiUrl,
        formData
      )
      .subscribe({

        next: (response) => {

          console.log(
            'Import Success:',
            response
          );

          alert(
            'Customer data imported successfully.'
          );

        },

        error: (err) => {

          console.error(
            'Import Error:',
            err
          );

          alert(
            'Import failed.'
          );

        }

      });

  }

}