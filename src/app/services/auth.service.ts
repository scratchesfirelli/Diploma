import { User } from './../models/user';
import { Observable } from 'rxjs/Observable';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';
import { tokenNotExpired } from "angular2-jwt";

@Injectable()
export class AuthService {
  baseUrl = 'http://localhost:5000/api/account';
  //baseUrl = 'http://localhost:50707/api/account';
  token: any;

  constructor(private http: Http) { }

  isAdmin() {
    return localStorage.getItem('role') == 'admin';
  }

  loggedIn() {
    return tokenNotExpired();
  }

  register(email: String, password: String) {
    const url = this.baseUrl + '/register';
    const user = {
      Email: email,
      Password: password
    }
    return this.http.post(url, JSON.stringify(user), this.getRequestOptions())
      .map(result => result.json());
  }

  getProfile(): Observable<User> {
    this.loadToken();
    const url = this.baseUrl + '/getProfile';
    let requestOptions = this.getRequestOptions();
    requestOptions.headers.append('Authorization', `Bearer ${this.token}`);
    console.log(requestOptions);
    return this.http.get(url, requestOptions)
      .map(result => result.json());
  };

  logIn(email: String, password: String) {
    const url = this.baseUrl + '/login';
    const user = {
      Email: email,
      Password: password
    }
    return this.http.post(url, JSON.stringify(user), this.getRequestOptions())
      .map(result => {
        let token = result.json() && result.json().token;
        let role = result.json() && result.json().role;
        if (token) {
          this.token = token;
          localStorage.setItem('user', JSON.stringify({ email: user.Email }));
          localStorage.setItem('token', token);
          localStorage.setItem('role', role);
          return true;
        } else {
          return false;
        }
      });
  }

  logOut() {
    this.token = null;
    localStorage.clear();
  }

  loadToken() {
    this.token = localStorage.getItem('token');
  }

  private getRequestOptions() {
    return new RequestOptions({
      headers: new Headers({
        "Content-Type": "application/json",
      })
    });
  }
}
