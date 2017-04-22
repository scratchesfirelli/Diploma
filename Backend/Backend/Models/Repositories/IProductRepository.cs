using Backend.Models;
using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Models.Repositories
{
  public interface IProductRepository
  {
    ProductsList GetProductsList(int page, int pageSize);
    Product GetById(Guid id);
    bool Create(Product product);
    bool Remove(string id);
    bool Update(string id, Product product);
    IEnumerable<ProductMaterial> GetProductMaterials();
    IEnumerable<ProductType> GetProductTypes();
  }
}
