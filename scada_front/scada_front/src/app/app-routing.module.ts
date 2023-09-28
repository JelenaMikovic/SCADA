import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import {DbManagerComponent} from "./db-manager/db-manager.component";
import {TrendingComponent} from "./trending/trending.component";
import {AlarmDisplayComponent} from "./alarm-display/alarm-display.component";
import {ReportsComponent} from "./reports/reports.component";

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  {path:'db-manager',component:DbManagerComponent},
  {path:'trending',component:TrendingComponent},
  {path:'alarm-display',component:AlarmDisplayComponent},
  {path:'reports',component:ReportsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
