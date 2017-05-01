import { Http, Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthService {
  baseUrl = 'http://localhost:5000/api/account';

  constructor(private http: Http ) {}

  register(email: String, password: String) {
    const url = this.baseUrl+'/register';
    const user = {
      Email: email,
      Password: password
    }
    return this.http.post(url, JSON.stringify(user), this.getRequestOptions())
      .map(result => result.json());
  }

  logIn(email: String, password: String) {
    const url = this.baseUrl+'/login';
    const user = {
      Email: email,
      Password: password
    }
    return this.http.post(url, JSON.stringify(user), this.getRequestOptions())
      .map(result => result.json());
  }

  private getRequestOptions() {
        return new RequestOptions({
            headers: new Headers({
                "Content-Type": "application/json",
            })
        });
    }
}
