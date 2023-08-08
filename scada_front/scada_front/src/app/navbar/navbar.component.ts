import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { RecursiveAstVisitor } from '@angular/compiler';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isAuth: boolean;
  constructor(private router: Router) {
    this.isAuth = true;
  }

  public ngOnInit(): void {
    
  }

  isAuthenticated(): boolean {
    return this.isAuth;
  }

  public signOut(): void {
    this.router.navigate(['/login']);
  }
}
