import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import {DbManagerComponent} from "./db-manager/db-manager.component";
import {TrendingComponent} from "./trending/trending.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  {path:'db-manager',component:DbManagerComponent},
  {path:'trending',component:TrendingComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
