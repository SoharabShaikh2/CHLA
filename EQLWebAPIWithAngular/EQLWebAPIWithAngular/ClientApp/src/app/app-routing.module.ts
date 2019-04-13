import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from "./components/login/login.component";
import { LoginByUserComponent } from "./components/login-by-user/login-by-user.component";
import { OrganisationListComponent } from "./components/organisation-list/organisation-list.component";
import { AdminOneComponent } from "./components/admin-one/admin-one.component";
import { DrModuleComponent } from "./components/dr-module/dr-module.component";
import { DrModuleTabComponent } from "./components/dr-module-tab/dr-module-tab.component";
import { ForgotPasswordComponent } from "./components/forgot-password-email/forgot-password.component";
import { ForgotPasswordComponentReset } from "./components/forgot-password/forgot-password.component";



const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'userLogin', component: LoginByUserComponent},
  {path: 'organizationList', component: OrganisationListComponent},
  {path: 'admin', component: AdminOneComponent},
  {path: 'drModule', component: DrModuleComponent},
  { path: 'drModuletab', component: DrModuleTabComponent },
  { path: 'forgotPassword', component: ForgotPasswordComponent },
  { path: 'resetPassword', component: ForgotPasswordComponentReset },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
