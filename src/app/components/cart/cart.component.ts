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

  public cartItems: Observable<Product[]>;

  constructor(private cartService: CartService) {
    this.cartItems = this.cartService.getItems();
  }

  ngOnInit() {
  }

}
