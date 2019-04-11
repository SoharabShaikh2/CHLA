import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MDBBootstrapModule, WavesModule, ButtonsModule, CardsFreeModule } from 'angular-bootstrap-md';
import { MatTabsModule, MatDialogModule, MatSortModule, MatNativeDateModule, MatDatepickerModule  } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
//import {MatDatepickerModule} from '@angular/material/datepicker';

import {MatButtonModule} from '@angular/material/button';


import { LoginComponent } from './components/login/login.component';
import { LoginByUserComponent } from './components/login-by-user/login-by-user.component';
import { OrganisationListComponent } from './components/organisation-list/organisation-list.component';
import { AdminOneComponent } from './components/admin-one/admin-one.component';
import { DrModuleComponent } from './components/dr-module/dr-module.component';
import { MatDialogComponent } from './components/mat-dialog/mat-dialog.component';
import { DrModuleTabComponent } from './components/dr-module-tab/dr-module-tab.component';

import { Globals } from '../app/global';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LoginByUserComponent,
    OrganisationListComponent,
    AdminOneComponent,
    DrModuleComponent,
    MatDialogComponent,
    DrModuleTabComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    WavesModule, ButtonsModule, CardsFreeModule,
    MatTabsModule, MatDialogModule, MatSortModule,
    BrowserAnimationsModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MDBBootstrapModule.forRoot(),
    HttpClientModule,
  ],
  schemas: [NO_ERRORS_SCHEMA],
  providers: [Globals, DatePipe],
  bootstrap: [AppComponent],
  entryComponents:[MatDialogComponent],
})
export class AppModule { }
