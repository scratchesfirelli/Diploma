using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Backend.Controllers
{
  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  public class OrderController : Controller
  {
    private IOrderRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly UserManager<User> _userManager;

    public JsonSerializerSettings Settings
    {
      get
      {
        return new JsonSerializerSettings()
        {
          Formatting = Formatting.Indented,
          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
      }
    }
    public OrderController(IOrderRepository repository, IHttpContextAccessor context, UserManager<User> userManager)
    {
      _repository = repository;
      _context = context;
      _userManager = userManager;
    }

    /// <summary>
    /// POST: api/order/create
    /// </summary>
    /// <returns>Creates a new order and returns creation result</returns>
    [HttpPost("Create"), Authorize]
    public async Task<JsonResult> CreateOrderAsync([FromBody]IEnumerable<OrderProduct> orderProducts)
    {
      bool res = false;
      var user = await _userManager.FindByEmailAsync(_context.HttpContext.User.Identity.Name);
      res = _repository.Create(orderProducts, user);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, Settings);
    }

    /// <summary>
    /// Get: api/order/getorders
    /// </summary>
    /// <returns>Creates a new order and returns creation result</returns>
    [HttpGet("GetOrders"), Authorize]
    public async Task<JsonResult> GetOrdersAsync()
    {
      var user = await _userManager.FindByEmailAsync(_context.HttpContext.User.Identity.Name);
      var orders = _repository.GetUsersOrders(user);
      return new JsonResult(orders, Settings);
    }
  }
}