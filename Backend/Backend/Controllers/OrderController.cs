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
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Backend.Controllers
{
  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  [AllowAnonymous]
  public class OrderController : Controller
  {
    private IOrderRepository _repository;
    private readonly IHttpContextAccessor _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

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
    public OrderController(
      IOrderRepository repository, 
      IHttpContextAccessor context,
      UserManager<User> userManager,
      RoleManager<IdentityRole> roleManager)
    {
      _repository = repository;
      _context = context;
      _userManager = userManager;
      _roleManager = roleManager;
    }

    /// <summary>
    /// POST: api/order/create
    /// </summary>
    /// <returns>Creates a new order and returns creation result</returns>
    [HttpPost("Create"), Authorize(Policy = "Customer")]
    public async Task<JsonResult> CreateOrderAsync([FromBody]IEnumerable<OrderProduct> orderProducts)
    {
      bool res = false;
      var user = await _userManager.FindByEmailAsync(_context.HttpContext.User.Identity.Name);
      res = _repository.Create(orderProducts, user);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, Settings);
    }

    /// <summary>
    /// POST: api/order/create
    /// </summary>
    /// <returns>Creates a new order and returns creation result</returns>
    [HttpPost("Complete"), Authorize(Policy = "Customer")]
    public JsonResult Complete([FromBody]Order order)
    {
      order.CompleteDate = DateTime.Now;
      var res = _repository.Complete(order);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, Settings);
    }

    /// <summary>
    /// Get: api/order/getorders/{n}/{n}
    /// </summary>
    /// <returns>Creates a new order and returns creation result</returns>
    [HttpGet("GetOrders/{page}/{pageSize}"), Authorize]
    public async Task<JsonResult> GetOrdersAsync(int page, int pageSize)
    {
      var user = await _userManager.FindByEmailAsync(_context.HttpContext.User.Identity.Name);
      if (await _userManager.IsInRoleAsync(user, "admin"))
      {
        return new JsonResult(_repository.GetOrders(null, page, pageSize), Settings);
      }
      else
      {
        return new JsonResult(_repository.GetOrders(user, page, pageSize), Settings);
      }
    }
  }
}