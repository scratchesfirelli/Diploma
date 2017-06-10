import { OrderProduct } from './../models/orderProduct';
import { Product } from './../models/product';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from "rxjs";

@Injectable()
export class CartService {
  private itemsInCartSubject: BehaviorSubject<OrderProduct[]> = new BehaviorSubject([]);
  private itemsInCart: OrderProduct[] = [];

  constructor() {
    this.itemsInCartSubject.subscribe(_ => this.itemsInCart = _);
  }

  public addToCart(item: Product) {
    const currentItems = [...this.itemsInCart];
    const itemsWithProductId = currentItems
      .filter(orderProducts => orderProducts.Product.Id === item.Id);
    if(itemsWithProductId.length == 0) {
      var orderProduct = new OrderProduct();
      orderProduct.Amount = 1;
      orderProduct.Product = item;
      orderProduct.ProductId = item.Id;
      this.itemsInCartSubject.next([...currentItems, orderProduct]);
    } else {
      itemsWithProductId[0].Amount += 1;
    }
    this.updateValues();
  }

  public getItems(): Observable<OrderProduct[]> {
    return this.itemsInCartSubject;
  }

  public removeFromCart(item: Product) {
    const currentItems = [...this.itemsInCart];
    const itemsWithProductId = currentItems
      .filter(orderProducts => orderProducts.Product.Id === item.Id);
    if(itemsWithProductId[0].Amount > 1) {
      var amount =  itemsWithProductId[0].Amount.toFixed();
      itemsWithProductId[0].Amount -= 1;
    } else {
      var itemsWithoutRemoved = currentItems.filter(orderProducts => orderProducts.Product.Id !== item.Id);
      this.itemsInCartSubject.next(itemsWithoutRemoved);
    }
    this.updateValues();
  }

  private updateValues() {
    var values = [...this.itemsInCart];
    this.itemsInCartSubject.next(values);
  }

  public clear() {
    this.itemsInCartSubject.next([]);
    this.itemsInCart = [];
  }

  public getTotalAmount(): Observable<number> {
    return this.itemsInCartSubject.map((items: OrderProduct[]) => {
      return items.reduce((prev, curr: OrderProduct) => {
        return prev + (+curr.Product.Price * +curr.Amount);
      }, 0);
    });
  }
}
