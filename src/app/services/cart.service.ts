import { Product } from './../models/product';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from "rxjs";

@Injectable()
export class CartService {
  private itemsInCartSubject: BehaviorSubject<Product[]> = new BehaviorSubject([]);
  private itemsInCart: Product[] = [];

  constructor() {
    this.itemsInCartSubject.subscribe(_ => this.itemsInCart = _);
  }

  public addToCart(item: Product) {
    this.itemsInCartSubject.next([...this.itemsInCart, item]);
  }

  public getItems(): Observable<Product[]> {
    return this.itemsInCartSubject;
  }

  public clear() {
    this.itemsInCartSubject.next([]);
    this.itemsInCart = [];
  }

  public getTotalAmount(): Observable<number> {
    return this.itemsInCartSubject.map((items: Product[]) => {
      return items.reduce((prev, curr: Product) => {
        return prev + +curr.Price;
      }, 0);
    });
  }
}
