using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderManagementSystem.Models.Repositories;
using OrderManagementSystem.Models;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers
{
  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  public class ProductController : Controller
  {
    private IProductRepository repository;
    public JsonSerializerSettings settings
    {
      get
      {
        return new JsonSerializerSettings()
        {
          Formatting = Formatting.Indented
        };
      }
    }
    public ProductController(IProductRepository repository)
    {
      this.repository = repository;
    }

    /// <summary>
    /// GET: api/product/getproducts/{n}
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>An array of {n} Json-serialized objects</returns>
    [HttpGet("GetProducts/{n}")]
    public JsonResult GetProducts(int? n)
    {
      int num = n ?? 10;
      var res = repository.GetProducts(num);
      return new JsonResult(res, settings);
    }

    /// <summary>
    /// GET: api/product/getproducts/{n}
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>An array of {n} Json-serialized objects</returns>
    [HttpGet("GetById/{id}")]
    public JsonResult GetById(int id)
    {
      return new JsonResult(repository.GetById(id), settings);
    }

    /// <summary>
    /// GET: api/product/getproducts/{n}
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>An array of {n} Json-serialized objects</returns>
    [HttpGet("GetTypes/{type}")]
    public JsonResult GetTypes(string type)
    {
      return new JsonResult(repository.GetTypes(type), settings);
    }

    /// <summary>
    /// POST: api/product/create
    /// </summary>
    /// <returns>Creates a new product and returns creation result</returns>
    [HttpPost("Create")]
    public JsonResult Create([FromBody]Product product)
    {
      var res = repository.Create(product);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, settings);
    }
  }
}
