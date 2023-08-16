import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../services/auth.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      //email: ['', [Validators.required, Validators.email]],
      //password: ['', Validators.required]
    });
  }

  register() {
    if (this.registerForm.valid) {
      const email = this.registerForm.value.email;
      const password = this.registerForm.value.password;

      if (this.authService.login(email, password)) {
        console.log('Login successful');
        // You can navigate to another page or perform other actions here
      } else {
        console.log('Login failed');
        // Display an error message or take appropriate action
      }
    }
  }
}
