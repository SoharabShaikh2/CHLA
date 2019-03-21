import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, retry, catchError } from 'rxjs/operators';
import {LoginData} from '../all-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      //'Authorization': 'my-auth-token'
    })
  };
  constructor(private http : HttpClient) { }

  checkUser(email,password){
 //   return this.http.get<string>('http://localhost:52814/api/user/UserLogin?emailId=iamsoharab@gmail.com&password=admin');
let data:LoginData= new LoginData();
data.Password=password;
data.Username=email;
    return this.http.post<any>('/api/user/UserLoginApp/', data,this.httpOptions)
    .pipe(
      map(responseData=>{return responseData })
    );
  }
}
