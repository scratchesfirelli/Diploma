import { CartService } from './../../services/cart.service';
import { AuthService } from './../../services/auth.service';
import { ProductsList } from '../../models/productsList';
import { PagingInfo } from '../../models/pagingInfo';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
    pageSize: Number = 10;
    products: Product[];
    pagingInfo: PagingInfo;
    page: Number;
    imgPath: String = "../../../../Backend/Backend/Media/";


    constructor(
        private productService: ProductService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private authService: AuthService,
        private cartService: CartService) { }

    ngOnInit() {
        this.activatedRoute.params.subscribe(params => {
            this.page = +params['page'];
            this.getProducts();
        })
    }

    addToCart(item: Product) {
        this.cartService.addToCart(item);
    }

    getProducts() {
        this.productService.getProductsList(this.page, +this.pageSize)
            .subscribe(res => {
                this.products = res.Products;
                this.pagingInfo = res.PagingInfo;
            });
    }
}
