import { CartService } from './../../services/cart.service';
import { AuthService } from './../../services/auth.service';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from './../../models/product';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Modal } from "ng2-modal";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  product: Product;
  imgPath: String = "../../../../Backend/Backend/Media/";
  @ViewChild('myModal') myModal: Modal;

  constructor(
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private cartService: CartService) { }

  ngOnInit() {
    const id = this.activatedRoute.snapshot.params['id'];
    this.productService.getById(+id)
      .subscribe(product => this.product = product);
  }

  addToCart() {
    this.cartService.addToCart(this.product);
  }

  removeProduct() {
    this.productService.removeProduct(this.product)
      .subscribe(data => {
        this.myModal.close();
        if(data.success) this.router.navigate(['product/list/1']);
      });
  }
}
