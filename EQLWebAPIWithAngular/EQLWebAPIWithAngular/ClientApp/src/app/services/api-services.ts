import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, retry, catchError } from 'rxjs/operators';
import { Organization } from '../services/all-model';

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      //'Authorization': 'my-auth-token'
    })
  };
  constructor(private http: HttpClient) { }

  getOrganization() {
    return this.http.get<Array<Organization>>('/api/Organization/OrganizationList/', this.httpOptions)
      .pipe(
        map(responseData => { return responseData })
      );
  }

  getOrganizationUsers(id) {
    let data: number;
    data = id;
    return this.http.post<any>('/api/Organization/OrganizationUsersList', data, this.httpOptions)
      .pipe(
        map(responseData => { return responseData })
      );
  }

  getOrganizationUsersWithSerch(id, input) {
    let wid: number;
    let text: string;
    wid = id;
    text = input;

    var data = {
      "id": wid,
      "text": text
    };
    let body = JSON.stringify(data)

    return this.http.post<any>('/api/Organization/OrganizationUsersListSearch', body, this.httpOptions)
      .pipe(
        map(responseData => { return responseData })
      );
  }

  //getOrganizationUsersResult(userId) {


  //  let wid: number;
  //  let text: string;
  //  wid = 0;
  //  text = userId;

  //  var data = {
  //    "id": wid,
  //    "text": text
  //  };
  //  let body = JSON.stringify(data)

  //  return this.http.post<any>('/api/Organization/GetUserResult', body, this.httpOptions)
  //    .pipe(
  //      map(responseData => { return responseData })
  //    );
  //}

  getOrganizationUsersResult(userId,dateTime,Input) {

    let wid: number;
    let text: string;
    wid = 0;
    text = userId;

    var data = {
      "id": wid,
      "text": text,
      "dateTime": dateTime,
      "input": Input
    };
    let body = JSON.stringify(data)

    return this.http.post<any>('/api/Organization/GetUserResult', body, this.httpOptions)
      .pipe(
        map(responseData => { return responseData })
      );
  }


}
