import { Component, OnInit } from '@angular/core';
import { MatNativeDateModule, MatDialog} from '@angular/material';
import { MatDialogComponent } from '../mat-dialog/mat-dialog.component'
@Component({
  selector: 'app-dr-module',
  templateUrl: './dr-module.component.html',
  styleUrls: ['./dr-module.component.scss']
})
export class DrModuleComponent implements OnInit {
  constructor(public matDialog: MatDialog) { }
  calender:any;
  ngOnInit() {
    console.log(this.calender);
  }
  close(){
    this.matDialog.open(MatDialogComponent, {
      width:"700px",
      height: "500px"
    })
  }
}
