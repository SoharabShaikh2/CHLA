import { Injectable } from '@angular/core';
import { UserResult } from '../app/services/all-model';

@Injectable()
export class Globals {
  loginStatus: boolean = false;
  loginUserId: number = 0;
  loginUserType: number = 0;
  loginOrganizationId: number = 0;
  loginUserName: string = null;
  loginUserFullName: string = null;
  loginOrganizationName: string = null;

  userResult: Array<UserResult>;

  mainAdmin: number = 4;
  hospitalAdmin: number = 1;
  hospitalUser: number = 3;

  selectDate: any = null;
  
}
