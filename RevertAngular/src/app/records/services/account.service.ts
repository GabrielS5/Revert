import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl: string;

  constructor(private http: HttpClient) {}

  getAllUsers() {
    return this.http.get(this.baseUrl);
  }

  logIn(credentials) {
    return this.http.post(this.baseUrl, {credentials});
  }

  logout() {
   // logging out
  }
}
