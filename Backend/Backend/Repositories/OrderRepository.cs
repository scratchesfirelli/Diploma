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

    public OrdersList GetOrders(User user, int page, int pageSize)
    {
      IQueryable<Order> orders;
      if(user != null)
      {
        orders = _db.Orders
          .Where(order => order.User == user)
          .Include(order => order.OrderProducts)
          .ThenInclude(orderProduct => orderProduct.Product);
      }
      else
      {
        orders = _db.Orders
          .Include(order => order.User)
          .Include(order => order.OrderProducts)
          .ThenInclude(orderProduct => orderProduct.Product);
      }
      var startIndex = (page - 1) * pageSize;
      var totalItems = orders.Count();
      OrdersList ordersList = new OrdersList
      {
        Orders = orders
                    .OrderByDescending(order => order.AddDate)
                    .Skip(startIndex)
                    .Take(pageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = totalItems,
          StartIndex = startIndex,
          EndIndex = Math.Min(startIndex + pageSize + 1, totalItems - 1)
        }
      };
      return ordersList;
    }

    public bool Complete(Order order)
    {
      try
      {
        _db.Entry<Order>(order).State = EntityState.Modified;
        _db.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
