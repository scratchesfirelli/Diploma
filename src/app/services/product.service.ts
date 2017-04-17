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

    getProducts(num?: Number): Observable<Product[]> {
        var url = this.baseUrl+'/getproducts';
        url = num ? url+`/${num}`: url;
        return this.http.get(url)
            .map(res => res.json());
            //.catch(error => {console.log(error); Observable.throw(error.json().error || "Server error")});
    }
    ///
    getById(id: Number): Observable<Product> {
        var url = this.baseUrl+`/getById/${id}`;
        return this.http.get(url)
            .map(res => res.json());
    }
    getProductTypes(): Observable<ProductType> {
        var url = this.baseUrl+`/getProductTypes`;
        return this.http.get(url)
            .map(res => res.json());
            //.catch(error => {console.log(error); Observable.throw(error.json().error || "Server error")});
    }

    getProductMaterials(): Observable<ProductMaterial> {
        var url = this.baseUrl+`/getProductMaterials`;
        return this.http.get(url)
            .map(res => res.json());
    }

    addProduct(product: Product) {
        var url = this.baseUrl+`/create`;
        console.log(product);
        console.log(url);
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
