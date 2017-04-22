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
    pageSize: Number = 1;
    products: Product[];
    pagingInfo: PagingInfo;
    page: Number;
    constructor(
        private productService: ProductService,
        private activatedRoute: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.activatedRoute.params.subscribe(params => {
            this.page = +params['page'];
            this.productService.getProductsList(this.page, this.pageSize)
            .subscribe(res => {
                this.products = res.Products;
                this.pagingInfo = res.PagingInfo;
            });
        })
    }

    onRouteChanged(path) {
        console.log('in init');
        let page = this.activatedRoute.snapshot.params['page'];
        page = page ? page : 1;
        console.log(page);
        this.productService.getProductsList(page, this.pageSize)
            .subscribe(res => {
                this.products = res.Products;
                this.pagingInfo = res.PagingInfo;
            });
    }
}
