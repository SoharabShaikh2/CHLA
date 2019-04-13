import { Component, OnInit } from '@angular/core';
import { Globals } from '../../global';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from 'src/app/services/api-services';
import { DatePipe } from '@angular/common';
import { UserResult } from '../../services/all-model';
import { retry } from 'rxjs/operators';

@Component({
  selector: 'app-dr-module-tab',
  templateUrl: './dr-module-tab.component.html',
  styleUrls: ['./dr-module-tab.component.scss']
})
export class DrModuleTabComponent implements OnInit {

  resId: string;
  userRes: UserResult;
  details: any;
  quali: any;
  quan: any;
  userName: string;


  constructor(private globals: Globals, private router: Router, private apiService: ApiService, private route: ActivatedRoute, private datePipe: DatePipe) { }

  ngOnInit() {
    if (!this.globals.loginStatus) {
      this.router.navigate(['/']);
    }
    this.resId = this.route.snapshot.paramMap.get('resId');
    this.userName = this.route.snapshot.paramMap.get('userName');

    //let userData = this.globals.userResult;
    this.userRes = this.globals.userResult.find(x => x.id == Number(this.resId));
    let mainData = JSON.parse(this.userRes.resultJSon);
    this.details = mainData.Details;
    this.quali = mainData.Qualitative;
    this.quan = mainData.Quantitative;


    console.log(mainData);
  }

  formatDate(time) {
    let newDate = new Date(time);
    let mainDate = this.datePipe.transform(newDate, 'yyyy-MM-dd');
    return mainDate;
  }
  formatTime(time) {
    let newDate = new Date(time);
    let mainDate = this.datePipe.transform(newDate, 'HH:mm:ss a');
    return mainDate;
  }

  convertTimeSpanToMin(e) {
    let time = Number(e);
    // Hours, minutes and seconds
    var hrs = ~~(time / 3600);
    var mins = ~~((time % 3600) / 60);
    var secs = ~~time % 60;

    // Output like "1:01" or "4:03:59" or "123:03:59"
    var ret = "";

    if (hrs > 0) {
      ret += "" + hrs + ":" + (mins < 10 ? "0" : "");
    }

    ret += "" + mins + ":" + (secs < 10 ? "0" : "");
    ret += "" + secs;
    return ret;
  }

}
