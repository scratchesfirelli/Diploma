using System.Collections.Generic;

namespace OrderManagementSystem.Models.Repositories
{
  public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int n);
        Product GetById(int id);
        bool Create(Product product);
        bool Remove(string id);
        bool Update(string id, Product product);
        IEnumerable<string> GetTypes(string type);
    }
}
