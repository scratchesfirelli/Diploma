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
    getTypes(type: String): Observable<String> {
        var url = this.baseUrl+`/getTypes/${type}`;
        return this.http.get(url)
            .map(res => res.json());
            //.catch(error => {console.log(error); Observable.throw(error.json().error || "Server error")});
    }

    addProduct(product: Product) {
        var url = this.baseUrl+`/create`;
/*        const prod: Product = {
            Id: null,
            Description: product.Description,
            Title: product.Title,
            Price: product.Price,
            Type: product.Type,
            Height: product.Height,
            CreateDate: product.CreateDate,
            Weight: product.Weight,
            Rating: product.Rating,
            Width: product.Width,
            Length: product.Length

        }*/
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
