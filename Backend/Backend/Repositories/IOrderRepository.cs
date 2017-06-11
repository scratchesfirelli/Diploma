using Backend.Models;
using System.Collections.Generic;

namespace Backend.Repositories
{
  public interface IOrderRepository
  {
    OrdersList GetOrders(User user, int page, int pageSize);
    bool Complete(Order order);
    bool Create(IEnumerable<OrderProduct> order, User user);
  }
}
