using Backend.Models;
using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Models.Repositories
{
  public interface IProductRepository
  {
    IEnumerable<Product> GetProducts(int n);
    Product GetById(Guid id);
    bool Create(Product product);
    bool Remove(string id);
    bool Update(string id, Product product);
    IEnumerable<ProductMaterial> GetProductMaterials();
    IEnumerable<ProductType> GetProductTypes();
  }
}
