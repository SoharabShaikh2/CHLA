import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {Globals} from '../../global';
import { ApiService } from 'src/app/services/api-services';

@Component({
  selector: 'app-admin-one',
  templateUrl: './admin-one.component.html',
  styleUrls: ['./admin-one.component.scss']
})
export class AdminOneComponent implements OnInit {

  constructor(private globals: Globals,private router: Router,private apiService : ApiService) { }

  ngOnInit() {
    if(!this.globals.loginStatus)
    {
      this.router.navigate(['/']);
    }
    this.apiService.getOrganizationUsers(1).subscribe(data => {
      console.log(data);
    });
  }

}
