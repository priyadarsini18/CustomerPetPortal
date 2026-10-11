import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  private apiUrl =
    'http://localhost:5278/api/customers';

  constructor(
    private http: HttpClient
  ) { }

  getClaims(
    customerId: number
  ): Observable<any[]> {

    return this.http.get<any[]>(
      `${this.apiUrl}/${customerId}/claims`
    );
  }
}