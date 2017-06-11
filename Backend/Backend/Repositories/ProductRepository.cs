using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.DbContext;

namespace Backend.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private OmsContext _db { get; set; }
    public ProductRepository(OmsContext db)
    {
      _db = db;
    }

    public Product GetById(int id)
    {
      return _db.Products
        .Where(prod => prod.Id == id)
        .Include(prod => prod.Type)
        .Include(prod => prod.Material)
        .First();
    }

    public bool Create(Product product)
    {
      try
      {
        _db.Products.Add(product);
      }
      catch
      {
        return false;
      }
      _db.SaveChanges();
      return true;
    }

    public bool Remove(Product product)
    {
      try
      {
        _db.Products.Remove(product);
      }
      catch
      {
        return false;
      }
      _db.SaveChanges();
      return true;
    }

    public bool Update(Product product)
    {
      try
      {
        _db.Products.Update(product);
      }
      catch
      {
        return false;
      }
      _db.SaveChanges();
      return true;
    }
    public IEnumerable<ProductMaterial> GetProductMaterials()
    {
      return _db.ProductMaterials.OrderBy(material => material.Title).ToList();
    }

    public IEnumerable<ProductType> GetProductTypes()
    {
      return _db.ProductTypes.OrderBy(type => type.Title).ToList();
    }
    public ProductsList GetProductsList(int page, int pageSize)
    {
      var startIndex = (page - 1) * pageSize;
      var totalItems = _db.Products.Count();
      ProductsList productsList = new ProductsList
      {
        Products = this._db.Products
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

    public bool SaveMaterial(ProductMaterial material)
    {
      try
      {
        _db.ProductMaterials.Add(material);
        _db.SaveChanges();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool SaveType(ProductType type)
    {
      try
      {
        _db.ProductTypes.Add(type);
        _db.SaveChanges();
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
