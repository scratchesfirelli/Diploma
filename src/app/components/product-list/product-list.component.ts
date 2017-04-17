import { ActivatedRoute } from '@angular/router';
import { Product } from './../../models/product';
import { ProductService } from './../../services/product.service';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: Observable<Product[]>;
    constructor(private productService: ProductService, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        const num = this.activatedRoute.snapshot.params['num'];
        if(num) {
            this.products = this.productService.getProducts(num);
        } else {
            this.products = this.productService.getProducts();
        }
    }
}
