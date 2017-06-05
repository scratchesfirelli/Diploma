using System.Collections.Generic;

namespace Backend.Models.Repositories
{
  public interface IOrderRepository
  {
    IEnumerable<Order> GetUsersOrders(string id);
    bool CreateOrder(IEnumerable<Product> order);
  }
}
