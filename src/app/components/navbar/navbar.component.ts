import { OrderProduct } from './../../models/orderProduct';
import { Product } from './../../models/product';
import { Observable } from 'rxjs/Observable';
import { CartService } from './../../services/cart.service';
import { Router } from '@angular/router';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  userName: String;
  shoppingCartItems$: Observable<OrderProduct[]>;
  cartTotalPrice: Number;

  constructor(
    private authService: AuthService,
    private router: Router,
    private cartService: CartService) {
    this.shoppingCartItems$ = this.cartService.getItems();
  }

  ngOnInit() {
    this.authService.getProfile()
    this.cartService.getTotalAmount().subscribe(value => this.cartTotalPrice = value)
  }

  onLogoutClick() {
    this.authService.logOut();
    this.router.navigate(['login']);
  }
}
