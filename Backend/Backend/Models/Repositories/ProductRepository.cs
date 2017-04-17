using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementSystem.Models.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private OmsContext _db { get; set; }
        private DbSet<Product> Products { get; set; }
        public ProductRepository(OmsContext db)
        {
            _db = db;
            Products = db.Products;
        }
        public IEnumerable<Product> GetProducts(int n)
        {
            return Products.Take(n).ToList();
        }

        public Product GetById(int id)
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

        public IEnumerable<string> GetTypes(string type)
        {
            return Products.Where(product => product.Type.Contains(type)).ToList().Select(product => product.Type);
        }
    }
}
