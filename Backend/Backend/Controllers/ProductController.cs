using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Controllers
{
  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  [AllowAnonymous]
  public class ProductController : Controller
  {
    private IProductRepository repository;
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
    public JsonResult GetById(int id)
    {
      var product = repository.GetById(id);
      return new JsonResult(product, Settings);
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
    /// POST: api/product/delete
    /// </summary>
    /// <returns>Deletes a product and returns deletion result</returns>
    [HttpPost("RemoveProduct"), Authorize(Roles = "admin")]
    public JsonResult RemoveProduct([FromBody]Product product)
    {
      var res = repository.Remove(product);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, Settings);
    }

    /// <summary>
    /// POST: api/product/saveMaterial
    /// </summary>
    /// <returns>Saves product material</returns>
    [HttpPost("SaveMaterial"), Authorize(Roles = "admin")]
    public JsonResult SaveMaterial([FromBody]ProductMaterial material)
    {
      var res = repository.SaveMaterial(material);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, Settings);
    }

    /// <summary>
    /// POST: api/product/saveType
    /// </summary>
    /// <returns>Saves product type</returns>
    [HttpPost("SaveType"), Authorize(Roles = "admin")]
    public JsonResult SaveType([FromBody]ProductType type)
    {
      var res = repository.SaveType(type);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, Settings);
    }

    /// <summary>
    /// POST: api/product/create
    /// </summary>
    /// <returns>Creates a new product and returns creation result</returns>
    [HttpPost("SaveProduct"), Authorize(Roles = "admin")]
    public JsonResult SaveProduct([FromBody]Product product)
    {
      bool res = false;
      res = product.Id == 0 ? repository.Create(product) : repository.Update(product);
      return new JsonResult(res == true ? new { success = true } : new { success = false }, Settings);
    }
  }
}
