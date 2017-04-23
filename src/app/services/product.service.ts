import { ProductsList } from './../models/productsList';
import { ProductMaterial } from './../models/productMaterial';
import { ProductType } from './../models/productType';
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Product } from "../models/product";

import 'rxjs'

@Injectable()
export class ProductService {
    private baseUrl = 'http://localhost:5000/api/product';
    constructor(private http: Http) { }

    getProductsList(page: Number, pageSize: Number): Observable<ProductsList> {
        let url = this.baseUrl+`/GetProductsList/${page}/${pageSize}`;
        return this.http.get(url, this.getRequestOptions())
            .map(res => res.json());
    }

    getById(id: Number): Observable<Product> {
        let url = this.baseUrl+`/getById/${id}`;
        return this.http.get(url)
            .map(res => res.json());
    }

    getProductTypes(): Observable<ProductType> {
        let url = this.baseUrl+`/getProductTypes`;
        return this.http.get(url)
            .map(res => res.json());
    }

    getProductMaterials(): Observable<ProductMaterial> {
        let url = this.baseUrl+`/getProductMaterials`;
        return this.http.get(url)
            .map(res => res.json());
    }

    removeProduct(product: Product) {
        let url = this.baseUrl+`/removeProduct`;
        return this.http.post(url, JSON.stringify(product), this.getRequestOptions())
            .map(res => res.json());
    }

    saveProduct(product: Product) {
        let url = this.baseUrl+`/saveProduct`;
        return this.http.post(url, JSON.stringify(product), this.getRequestOptions())
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
