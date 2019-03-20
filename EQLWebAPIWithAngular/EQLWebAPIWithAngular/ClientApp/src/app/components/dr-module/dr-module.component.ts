import { Component, OnInit } from '@angular/core';
import { MatNativeDateModule, MatDialog} from '@angular/material';
import { MatDialogComponent } from '../mat-dialog/mat-dialog.component'
import {Globals} from '../../global';
import {Router} from "@angular/router";
@Component({
  selector: 'app-dr-module',
  templateUrl: './dr-module.component.html',
  styleUrls: ['./dr-module.component.scss']
})
export class DrModuleComponent implements OnInit {
  constructor(public matDialog: MatDialog,private globals: Globals,private router: Router) { }
  calender:any;
  ngOnInit() {
    if(!this.globals.loginStatus)
    {
      this.router.navigate(['/']);
    }
    console.log(this.calender);
  }
  close(){
    this.matDialog.open(MatDialogComponent, {
      width:"700px",
      height: "500px"
    })
  }
}
