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
    return this.authService.isLoggedIn();
  }

  hasUserRole(role: string): boolean {
    return this.authService.getUserRole() === role;
  }

  public signOut(): void {
    this.authService.logout()
    this.router.navigate(['/login']);
  }
}
