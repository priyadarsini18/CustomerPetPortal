import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PetService {

  private apiUrl =
    'http://localhost:5278/api/customers';

  constructor(
    private http: HttpClient
  ) { }

  getPets(
    customerId: number
  ): Observable<any[]> {

    return this.http.get<any[]>(
      `${this.apiUrl}/${customerId}/pets`
    );
  }

  addPet(
    customerId: number,
    pet: any
  ): Observable<any> {

    return this.http.post(
      `${this.apiUrl}/${customerId}/pets`,
      pet
    );
  }

  deactivatePet(
    customerId: number,
    petId: number
  ): Observable<any> {

    return this.http.delete(
      `${this.apiUrl}/${customerId}/pets/${petId}`
    );
  }
}