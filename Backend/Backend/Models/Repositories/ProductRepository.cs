using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;

namespace OrderManagementSystem.Models.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private OmsContext _db { get; set; }
    private DbSet<Product> Products { get; set; }
    private DbSet<ProductMaterial> ProductMaterials { get; set; }
    private DbSet<ProductType> ProductTypes { get; set; }
    public ProductRepository(OmsContext db)
    {
      _db = db;
      Products = db.Products;
      ProductMaterials = db.ProductMaterials;
      ProductTypes = db.ProductTypes;
    }

    public Product GetById(Guid id)
    {
      return Products.Where(prod => prod.Id == id).First();
    }

    public bool Create(Product product)
    {
      try
      {
        Products.Add(product);
      }
      catch
      {
        return false;
      }
      _db.SaveChanges();
      return true;
    }

    public bool Remove(string id)
    {
      throw new NotImplementedException();
    }

    public bool Update(string id, Product product)
    {
      throw new NotImplementedException();
    }
    public IEnumerable<ProductMaterial> GetProductMaterials()
    {
      return ProductMaterials.OrderBy(material => material.Title).ToList();
    }

    public IEnumerable<ProductType> GetProductTypes()
    {
      return ProductTypes.OrderBy(type => type.Title).ToList();
    }
    public ProductsList GetProductsList(int page, int pageSize)
    {
      var startIndex = (page - 1) * pageSize;
      var totalItems = Products.Count();
      ProductsList productsList = new ProductsList
      {
        Products = this.Products
                    .OrderByDescending(product => product.CreateDate)
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
      return productsList;
    }
  }
}
