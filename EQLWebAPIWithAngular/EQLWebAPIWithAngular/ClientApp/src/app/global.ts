import { Injectable } from '@angular/core';
import { UserResult } from '../app/services/all-model';

@Injectable()
export class Globals {
  loginStatus: boolean = false;

  userResult: Array<UserResult>;
  
}
