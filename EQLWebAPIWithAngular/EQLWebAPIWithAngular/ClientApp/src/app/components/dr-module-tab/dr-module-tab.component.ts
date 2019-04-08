import { Component, OnInit } from '@angular/core';
import { Globals } from '../../global';
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from 'src/app/services/api-services';
import { DatePipe } from '@angular/common';
import { UserResult } from '../../services/all-model';

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


  constructor(private globals: Globals, private router: Router, private apiService: ApiService, private route: ActivatedRoute, private datePipe: DatePipe) { }

  ngOnInit() {
    if (!this.globals.loginStatus) {
      this.router.navigate(['/']);
    }
    this.resId = this.route.snapshot.paramMap.get('resId');

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

}
