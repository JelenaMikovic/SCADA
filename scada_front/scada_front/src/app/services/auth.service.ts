import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly userKey = 'currentUser';

  login(email: string, password: string): boolean {
    if (email === 'user@example.com' && password === 'password') {
      var role = "admin"
      localStorage.setItem(this.userKey, JSON.stringify({ email, role }));
      return true;
    }
    return false;
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