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
    public ProductController(IProductRepository repository)
    {
      this.repository = repository;
    }

    /// <summary>
    /// GET: api/product/getproducts/{n}
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>An array of {n} Json-serialized objects</returns>
    [HttpGet("GetProductsList/{page}/{pageSize}")]
    public JsonResult GetProductsList(int page, int pageSize)
    {
      var res = repository.GetProductsList(page, pageSize);
      return new JsonResult(res, Settings);
    }

    /// <summary>
    /// GET: api/product/getmaterials
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>An array of {n} Json-serialized objects</returns>
    [HttpGet("GetProductMaterials")]
    public JsonResult GetProductMaterials()
    {
      return new JsonResult(repository.GetProductMaterials(), Settings);
    }

    /// <summary>
    /// GET: api/product/GetById/{id}
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>A serialized object taken by id</returns>
    [HttpGet("GetById/{id}")]
    public JsonResult GetById(Guid id)
    {
      return new JsonResult(repository.GetById(id), Settings);
    }

    /// <summary>
    /// GET: api/product/getproducttypes
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>An array of product types Json-serialized objects</returns>
    [HttpGet("GetProductTypes")]
    public JsonResult GetProductTypes(string type)
    {
      return new JsonResult(repository.GetProductTypes(), Settings);
    }

    /// <summary>
    /// POST: api/product/create
    /// </summary>
    /// <returns>Creates a new product and returns creation result</returns>
    [HttpPost("Create")]
    public JsonResult Create([FromBody]Product product)
    {
      var res = repository.Create(product);
      return new JsonResult(res == true ? new { success = "true" } : new { success = "false" }, Settings);
    }
  }
}
