import { Modal } from 'ng2-modal';
import { OrderProduct } from './../../models/orderProduct';
import { PagingInfo } from './../../models/pagingInfo';
import { Order } from './../../models/order';
import { Observable } from 'rxjs/Observable';
import { OrderService } from './../../services/order.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from "./../../services/auth.service";

@Component({
  selector: 'app-order',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  @ViewChild('orderModal') orderStatusModal: Modal;

  orders: Order[];
  pagingInfo: PagingInfo;
  page: Number;
  pageSize: Number = 10;
  order: Order;

  constructor(
    private router: Router,
    private orderService: OrderService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(params => {
      this.page = +params.get('page');
      this.getOrders();
    });
  }

  getOrders() {
    this.orderService.getOrders(this.page, +this.pageSize)
      .subscribe(res => {
        this.orders = res.Orders;
        this.pagingInfo = res.PagingInfo;
      });
  }

  totalPriceOfOrder(orderProducts) {
    return orderProducts.reduce((prev, curr: OrderProduct) => {
      return prev + (+curr.Product.Price * +curr.Amount);
    }, 0);
  }

  pupulateModalWithOrder(order: Order) {
    if (this.authService.isAdmin()) {
      this.order = order;
      this.orderStatusModal.open()
    }
  }

  completeOrder() {
    this.orderService.completeOrder(this.order).subscribe(data => {
      if (data.success == true) {
        this.orderStatusModal.close();
        this.getOrders();
      }
    });
  }
}
