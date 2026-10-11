import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private customerApiUrl =
    'http://localhost:5278/api/customers';

  constructor(
    private http: HttpClient
  ) { }

  login(
    email: string,
    password: string
  ): Observable<any> {

    return this.http.post<any>(
      `${this.customerApiUrl}/login`,
      {
        email,
        password
      }
    );

  }

  register(customer: any) {
 
return this.http.post<any>(
'http://localhost:5278/api/customers',
customer
);
 
}

}