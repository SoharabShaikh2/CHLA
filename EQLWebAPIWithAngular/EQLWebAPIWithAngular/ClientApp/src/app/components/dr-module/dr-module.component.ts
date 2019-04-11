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
  resultList: [];

  constructor(public matDialog: MatDialog, private datePipe: DatePipe, private globals: Globals, private router: Router, private apiService: ApiService, private route: ActivatedRoute) { }
  calender: any;
  ngOnInit() {
    if (!this.globals.loginStatus) {
      this.router.navigate(['/']);
    }
    //console.log(this.calender);

    this.userId = this.route.snapshot.paramMap.get('userId');
    this.orgaName = this.route.snapshot.paramMap.get('orgaName');

    console.log(this.userId);
    console.log(this.orgaName);

    this.apiService.getOrganizationUsersResult(this.userId).subscribe(data => {
      this.resultList = data;
      this.globals.userResult = data;
      console.log(data);
    });
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


  events: string[] = [];

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    //alert(event.value);
    //this.events.push(`${type}: ${event.value}`);
  }
}
