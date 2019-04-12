import { Component, OnInit } from '@angular/core';
import { MatNativeDateModule, MatDatepickerModule , MatDialog } from '@angular/material';
import { MatDialogComponent } from '../mat-dialog/mat-dialog.component'
import { Globals } from '../../global';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from 'src/app/services/api-services';
import { DatePipe } from '@angular/common';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
@Component({
  selector: 'app-dr-module',
  templateUrl: './dr-module.component.html',
  styleUrls: ['./dr-module.component.scss']
})
export class DrModuleComponent implements OnInit {
  userId: string;
  orgaName: string;
  userFullName: string;
  resultList: [];
  selectedDate: any;
  cday: any;
  cyear: any;
  cmonth: any;
  cdate: any;

  constructor(public matDialog: MatDialog, private datePipe: DatePipe, private globals: Globals, private router: Router, private apiService: ApiService, private route: ActivatedRoute) { }
  calender: any;
  ngOnInit() {
    if (!this.globals.loginStatus) {
      this.router.navigate(['/']);
    }
    //console.log(this.calender);

    this.userId = this.route.snapshot.paramMap.get('userId');
    this.orgaName = this.route.snapshot.paramMap.get('orgaName');
    this.userFullName = this.route.snapshot.paramMap.get('userName');

    this.apiService.getOrganizationUsersResult(this.userId).subscribe(data => {
      this.resultList = data;
      this.globals.userResult = data;
      console.log(data);
    });

    let dateC = new Date();
    this.cday = this.datePipe.transform(dateC, 'E');
    this.cmonth = this.datePipe.transform(dateC, 'LLLL');
    this.cyear = this.datePipe.transform(dateC, 'yyyy');
    this.cdate = this.datePipe.transform(dateC, 'dd');

  }
  close() {
    this.matDialog.open(MatDialogComponent, {
      width: "700px",
      height: "500px"
    })
  }

  formatTime(time) {
    let newDate = new Date(time);
    let mainDate = this.datePipe.transform(newDate, 'hh:mm a');
    return mainDate;
  }

  getResult(id) {
    if (id > 0) {
      this.router.navigate(['/drModuletab', { resId: id }]);
    }
  }


  onSelect(event) {
    this.selectedDate = event;
    console.log(event);
    let newDate = new Date(event);
    let mainDate = this.datePipe.transform(newDate, 'yyyy-MM-dd');

    this.cday = this.datePipe.transform(newDate, 'E');
    this.cmonth = this.datePipe.transform(newDate, 'LLLL');
    this.cyear = this.datePipe.transform(newDate, 'yyyy');
    this.cdate = this.datePipe.transform(newDate, 'dd');

    console.log(mainDate);
    
  }

  searchUsers(e) {
    console.log(e);
  }
}
