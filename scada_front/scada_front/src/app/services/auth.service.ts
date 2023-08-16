import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {UserReg} from "../dtos/userReg";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly userKey = 'currentUser';

  private headers = new HttpHeaders({
    'Content-Type': 'application/json',
    skip: 'true',
  });

  constructor(private http: HttpClient) {
  }

  login(email: string, password: string): boolean {
    if (email === 'user@example.com' && password === 'password') {
      var role = "admin"
      localStorage.setItem(this.userKey, JSON.stringify({ email, role }));
      return true;
    }
    return false;
  }

  register(body: UserReg): Observable<any> {
    return this.http.post<string>('http://localhost:8080/' + 'api/register', body, {
      headers: this.headers,
    });
  }


  logout(): void {
    localStorage.removeItem(this.userKey);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.userKey);
  }

  getUserRole(): string | null {
    const userData = localStorage.getItem(this.userKey);
    if (userData) {
      const user = JSON.parse(userData);
      return user.role || null;
    }
    return null;
  }
}
