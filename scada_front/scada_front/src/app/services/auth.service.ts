import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly userKey = 'currentUser';
  private baseUrl = 'http://localhost:5184/api/User';

  constructor(private http: HttpClient) { }

  authenticateUser(email: string, password: string): Observable<any> {
    console.log(email, password)
    return this.http.get<any>(`${this.baseUrl}/${email}/${password}`);
  }
  
  login(email: string, password: string): boolean {
    this.authenticateUser(email, password).subscribe(
      (response) => {console.log('User data:', response);

      if (response) {
        var role = "notAdmin"
        if (response.isAdmin) {
          role = "admin"
        }
        localStorage.setItem(this.userKey, JSON.stringify({ email, role }));
      }
      return true;
      },
      (error) => {
        console.error('Error:', error);
        return false;
      }
    );
  return true;
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