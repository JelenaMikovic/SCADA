import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  ngOnInit(): void {
  }

  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    //console.log(' succdasdadsadaessful');
    const email = this.loginForm.value.email;
    const password = this.loginForm.value.password;
    //console.log('Login succdasdadsadaessful');
    if (this.authService.login(email, password)) {
      //console.log('Login successful');
      // You can navigate to another page or perform other actions here
    } else {
      //console.log('Login failed');
      // Display an error message or take appropriate action
    }
  }
}
