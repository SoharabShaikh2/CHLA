import { Component, OnInit } from '@angular/core';
import {Globals} from '../../global';
import {Router} from "@angular/router";
import {ApiService} from "../../services/api-services";
import {Organization} from '../../services/all-model';

@Component({
  selector: 'app-organisation-list',
  templateUrl: './organisation-list.component.html',
  styleUrls: ['./organisation-list.component.scss']
})
export class OrganisationListComponent implements OnInit {
  orgaList : Array<Organization>;
  constructor(private globals: Globals,private router: Router,private apiService : ApiService) { }

  ngOnInit() {
    if(!this.globals.loginStatus)
    {
      this.router.navigate(['/']);
    }
    this.apiService.getOrganization().subscribe(data => {
      this.orgaList = data;
      console.log(this.orgaList);
    });
  }

  getOrganization(id){
    if(id > 0)
    {
      this.router.navigate(['/admin']);
    }
  }

}
