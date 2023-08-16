import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { RecursiveAstVisitor } from '@angular/compiler';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  
  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
  }

  isAuthenticated(): boolean {
    // Use your AuthService method or logic to check authentication status
    return this.authService.isLoggedIn();
  }

  hasUserRole(role: string): boolean {
    // Use your AuthService method to check the user's role
    return this.authService.getUserRole() === role;
  }

  public signOut(): void {
    this.router.navigate(['/login']);
  }
}
