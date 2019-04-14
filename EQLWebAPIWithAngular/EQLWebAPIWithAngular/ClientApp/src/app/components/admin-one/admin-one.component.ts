import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { Globals } from '../../global';
import { ApiService } from 'src/app/services/api-services';
import { OrganizationUsers } from '../../services/all-model';
import { UserService } from '../../services/user-service/user.service';

@Component({
  selector: 'app-admin-one',
  templateUrl: './admin-one.component.html',
  styleUrls: ['./admin-one.component.scss']
})
export class AdminOneComponent implements OnInit {
  orgaId: number;
  orgaName: string;
  adminCheck: string = null;
  adminView: boolean = false;

  orgaUsers: Array<OrganizationUsers>;
  constructor(private globals: Globals, private router: Router, private apiService: ApiService, private userService: UserService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.globals.selectDate = null;

    this.adminCheck = this.route.snapshot.paramMap.get('adminPortal');
    if (this.adminCheck == 'true') {
      this.globals.loginStatus = true;
      this.userService.checkUserFromAdmin(0).subscribe(data => {
        console.log(data);

        if (!data.status) {
          this.router.navigate(['/']);
        }
        else {
          this.adminView = true;
          this.globals.loginUserType = data.data.usertypeid;
          this.globals.loginOrganizationId = data.data.organizationid;
          this.globals.loginOrganizationName = data.data.organizationName;

          this.orgaId = this.globals.loginOrganizationId;
          this.orgaName = this.globals.loginOrganizationName;

          this.apiService.getOrganizationUsers(this.orgaId).subscribe(data => {
            this.orgaUsers = data;
            //this.orgaName = data[0].hospitalName;
            console.log('Users', this.orgaUsers);
            console.log('orgaName', this.orgaName);
          });
        }
      });

    }
    else {

      if (!this.globals.loginStatus) {
        this.router.navigate(['/']);
      }

      if (this.globals.loginUserType == this.globals.hospitalAdmin) {
        this.orgaId = this.globals.loginOrganizationId;
        this.orgaName = this.globals.loginOrganizationName;
      }
      else {
        this.orgaId = Number(this.route.snapshot.paramMap.get('orgaId'));
        this.orgaName = this.route.snapshot.paramMap.get('orgaName');
      }

      this.apiService.getOrganizationUsers(this.orgaId).subscribe(data => {
        this.orgaUsers = data;
        //this.orgaName = data[0].hospitalName;
        console.log('Users', this.orgaUsers);
        console.log('orgaName', this.orgaName);
      });
    }
  }

  searchUsers(e) {
    this.apiService.getOrganizationUsersWithSerch(this.orgaId, e.value).subscribe(data => {
      this.orgaUsers = data;
      //this.orgaName = data[0].hospitalName;
      console.log('Users', this.orgaUsers);
      console.log('orgaName', this.orgaName);
    });
  }

  getUserResult(e,f) {
    if (e != '') {
      //var orgaName = this.orgaList.find(x => x.id == id).name;
      this.router.navigate(['/drModule', { userId: e, orgaName: this.orgaName, userName: f }]);
    }
  }

}
