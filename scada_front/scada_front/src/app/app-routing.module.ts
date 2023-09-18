import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import {DbManagerComponent} from "./db-manager/db-manager.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  {path:'db-manager',component:DbManagerComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
