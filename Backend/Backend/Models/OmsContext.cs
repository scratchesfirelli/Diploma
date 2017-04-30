using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Models
{
  public class OmsContext : IdentityDbContext<AspNetUser>
  {
    public OmsContext(DbContextOptions<OmsContext> options)
            : base(options)
        { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMaterial> ProductMaterials { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    //  modelBuilder.Entity<Product>().HasIndex(p => p.ProductMaterialId).IsUnique(false);
    //  modelBuilder.Entity<Product>().HasIndex(p => p.ProductTypeId).IsUnique(false);
    }
  }
}
