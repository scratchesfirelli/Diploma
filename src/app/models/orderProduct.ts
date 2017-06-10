import { Product } from './product';
import { Order } from './order';
export class OrderProduct {
    OrderId: Number;
    ProductId: Number;
    Amount: number;
    Order: Order;
    Product: Product;
}