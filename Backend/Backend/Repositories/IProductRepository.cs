using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.Repositories
{
  public interface IProductRepository
  {
    ProductsList GetProductsList(int page, int pageSize);
    Product GetById(int id);
    bool Create(Product product);
    bool Remove(Product product);
    bool Update(Product product);
    IEnumerable<ProductMaterial> GetProductMaterials();
    IEnumerable<ProductType> GetProductTypes();
    bool SaveMaterial(ProductMaterial material);
    bool SaveType(ProductType type);
  }
}
