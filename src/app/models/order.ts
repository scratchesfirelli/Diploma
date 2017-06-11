import { PagingInfo } from './pagingInfo';
import { OrderProduct } from './orderProduct';
import { User } from './user';
export class Order {
    Id?: Number;
    AddDate: Date;
    CompleteDate?: Date;
    OrderProducts: OrderProduct[];
}