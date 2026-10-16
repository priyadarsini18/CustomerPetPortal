import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {

  private apiUrl =
    'http://localhost:5278/api/customers';

  constructor(
    private http: HttpClient
  ) { }

  getPolicies(
    customerId: number
  ): Observable<any[]> {

    return this.http.get<any[]>(
      `${this.apiUrl}/${customerId}/policies`
    );

  }

  deactivatePolicy(
    customerId: number,
    policyId: number
  ): Observable<any> {

    return this.http.put(
      `${this.apiUrl}/${customerId}/policies/${policyId}/deactivate`,
      {}
    );

  }

  deletePolicy(
    customerId: number,
    policyId: number
  ): Observable<any> {

    return this.http.delete(
      `${this.apiUrl}/${customerId}/policies/${policyId}`
    );

  }

}