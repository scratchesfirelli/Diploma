using Backend.DbContext;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private OmsContext _db { get; set; }
    public OrderRepository(OmsContext db)
    {
      _db = db;
    }
    public bool Create(IEnumerable<OrderProduct> orderProducts, User user)
    {
      try
      {
        var order = new Order();
        order.AddDate = DateTime.Now;
        order.CompleteDate = DateTime.Now;
        order.User = user;
        _db.Orders.Add(order);
        _db.SaveChanges();
        foreach (var item in orderProducts)
        {
          _db.OrderProducts.Add(new OrderProduct() { Amount = item.Amount, OrderId = order.Id, ProductId = item.Product.Id });
        }
        //_db.Orders.Add(new Order() { OrderProducts = orderProducts, User = user, AddDate = DateTime.Now });
        _db.SaveChanges();
      }
      catch
      {
        return false;
      }
      return true;
    }

    public IEnumerable<Order> GetUsersOrders(User user)
    {
      return _db.Orders
        .Where(order => order.User == user)
        .Include(order => order.OrderProducts)
        .ThenInclude(orderProduct => orderProduct.Product)
        .OrderByDescending(order => order.AddDate);
    }
  }
}
