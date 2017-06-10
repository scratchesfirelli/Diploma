import { AuthService } from './auth.service';
import { OrderProduct } from './../models/orderProduct';
import { Order } from './../models/order';
import { Observable } from 'rxjs/Observable';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class OrderService {
  private baseUrl = 'http://localhost:5000/api/order';
  //private baseUrl = 'http://localhost:50707/api/order';
  constructor(private http: Http, private authService: AuthService) { }

  create(orderProducts: OrderProduct[]) {
    let url = this.baseUrl + `/create`;
    let requestOptions = this.getRequestOptions();
    requestOptions.headers.append('Authorization', `Bearer ${this.authService.token}`);
    console.log(requestOptions);
    return this.http.post(url, JSON.stringify(orderProducts), requestOptions)
      .map(res => res.json());
  }

  getOrders(): Observable<Order[]> {
    let url = this.baseUrl+`/getOrders`;
    let requestOptions = this.getRequestOptions();
    requestOptions.headers.append('Authorization', `Bearer ${this.authService.token}`);
    return this.http.get(url, requestOptions)
      .map(res => res.json());
  }

  private getRequestOptions() {
    return new RequestOptions({
      headers: new Headers({
        "Content-Type": "application/json",
      })
    });
  }
}
