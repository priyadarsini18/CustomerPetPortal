import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExportService {

  private apiUrl =
    'http://localhost:5278/api/excel/customers/export';

  constructor(
    private http: HttpClient
  ) { }

  exportWorkbook(): Observable<Blob> {

    return this.http.get(
      this.apiUrl,
      {
        responseType: 'blob'
      }
    );

  }

}