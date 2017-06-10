using Backend.Models;
using System.Collections.Generic;

namespace Backend.Repositories
{
  public interface IOrderRepository
  {
    IEnumerable<Order> GetUsersOrders(User user);
    bool Create(IEnumerable<OrderProduct> order, User user);
  }
}
