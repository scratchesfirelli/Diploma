import { Router } from '@angular/router';
import { OrderService } from './../../services/order.service';
import { OrderProduct } from './../../models/orderProduct';
import { Product } from './../../models/product';
import { Observable } from 'rxjs/Observable';
import { CartService } from './../../services/cart.service';
import { Component, OnInit } from '@angular/core';
import { of } from "rxjs/observable/of";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  public cartItemsObs: Observable<OrderProduct[]>;

  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router) {
  }

  ngOnInit() {
    this.cartItemsObs = this.cartService.getItems();
  }

  removeOne(item: Product){
    this.cartService.removeFromCart(item);
  }

  addOne(item: Product){
    this.cartService.addToCart(item);
  }

  completeOrder(items: OrderProduct[]) {
    this.orderService.create(items).subscribe(data => {
      if (data.success) {
        this.cartService.clear();
        this.router.navigate(['/order']);
      }
    });;
  }
}
