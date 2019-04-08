import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { Globals } from '../../global';
import { ApiService } from 'src/app/services/api-services';
import { OrganizationUsers } from '../../services/all-model';

@Component({
  selector: 'app-admin-one',
  templateUrl: './admin-one.component.html',
  styleUrls: ['./admin-one.component.scss']
})
export class AdminOneComponent implements OnInit {
  orgaId: string;
  orgaName: string;

  orgaUsers: Array<OrganizationUsers>;
  constructor(private globals: Globals, private router: Router, private apiService: ApiService, private route: ActivatedRoute) {

  }

  ngOnInit() {

    this.orgaId = this.route.snapshot.paramMap.get('orgaId');
    this.orgaName = this.route.snapshot.paramMap.get('orgaName');
    console.log(this.orgaId);

    if (!this.globals.loginStatus) {
      this.router.navigate(['/']);
    }


    this.apiService.getOrganizationUsers(this.orgaId).subscribe(data => {
      this.orgaUsers = data;
      //this.orgaName = data[0].hospitalName;
      console.log('Users', this.orgaUsers);
      console.log('orgaName', this.orgaName);
    });
  }

  searchUsers(e) {
    this.apiService.getOrganizationUsersWithSerch(this.orgaId, e.value).subscribe(data => {
      this.orgaUsers = data;
      //this.orgaName = data[0].hospitalName;
      console.log('Users', this.orgaUsers);
      console.log('orgaName', this.orgaName);
    });
  }

  getUserResult(e) {
    if (e != '') {
      //var orgaName = this.orgaList.find(x => x.id == id).name;
      this.router.navigate(['/drModule', { userId: e, orgaName: this.orgaName }]);
    }
  }

}
