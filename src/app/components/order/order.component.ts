import { Order } from './../../models/order';
import { Observable } from 'rxjs/Observable';
import { OrderService } from './../../services/order.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  orders: Order[];

  constructor(
    private router: Router,
    private orderService: OrderService) { }

  ngOnInit() {
    this.orderService.getOrders()
      .subscribe(orders => this.orders = orders );
  }

}
