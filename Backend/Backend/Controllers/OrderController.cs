using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Backend.Models.Repositories;
using Newtonsoft.Json;

namespace Backend.Controllers
{
  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  [Authorize]
  public class OrderController : Controller
  {
    private IProductRepository repository;
    public JsonSerializerSettings Settings
    {
      get
      {
        return new JsonSerializerSettings()
        {
          Formatting = Formatting.Indented
        };
      }
    }
    public OrderController(IProductRepository repository)
    {
      this.repository = repository;
    }
  }
}