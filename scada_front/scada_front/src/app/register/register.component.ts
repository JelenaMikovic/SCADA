import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {HttpErrorResponse} from "@angular/common/http";
import {Router} from "@angular/router";
import {UserReg} from "../dtos/userReg";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      name: ['', Validators.required],
      lastname: ['', Validators.required]
    });
  }
  register(){
    if(this.registerForm.valid){
      const userReg: UserReg = {
        name: this.registerForm.value.name,
        surname: this.registerForm.value.surname,
        email: this.registerForm.value.email,
        password: this.registerForm.value.password
      }

      this.authService.register(userReg).subscribe({
        next: (result) =>{
          this.router.navigate(['login'])
        },
        error: (error)=>{
          if(error instanceof HttpErrorResponse){
            console.log("Error:" + error)
          }
        }
      })
    }else{
      console.log("Form error check fields.")
    }
  }
}
