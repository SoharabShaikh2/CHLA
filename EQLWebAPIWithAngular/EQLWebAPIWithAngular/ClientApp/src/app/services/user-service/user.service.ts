import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, retry, catchError } from 'rxjs/operators';
import { LoginData } from '../all-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      //'Authorization': 'my-auth-token'
    })
  };
  constructor(private http: HttpClient) { }

  checkUser(email, password) {
    let data: LoginData = new LoginData();
    data.Password = password;
    data.Username = email;
    return this.http.post<any>('/api/user/UserLoginApp/', data, this.httpOptions)
      .pipe(
        map(responseData => { return responseData })
      );
  }

  checkUserFromAdmin(UserId) {
    let data: LoginData = new LoginData();
    data.UserId = UserId;
    return this.http.post<any>('/api/user/UserLoginAppFromAdmin/', data, this.httpOptions)
      .pipe(
        map(responseData => { return responseData })
      );
  }
}
