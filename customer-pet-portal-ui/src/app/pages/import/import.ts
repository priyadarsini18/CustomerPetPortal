import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

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

  onFileSelected(event: Event): void {

    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }

  }

  importWorkbook(): void {

    if (!this.selectedFile) {

      alert('Please select an Excel file');
      return;

    }

    alert(
      'Ready to import: ' +
      this.selectedFile.name
    );

  }

}