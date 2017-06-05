using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
  public class OmsContext : IdentityDbContext<User>
  {
    public OmsContext(DbContextOptions<OmsContext> options)
            : base(options)
        { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMaterial> ProductMaterials { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<User>().ToTable("Users");
      modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
      modelBuilder.Entity<IdentityRole>().ToTable("Roles");
      modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
      modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
      modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
      modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

      modelBuilder.Entity<OrderProduct>()
        .HasKey(op => new { op.OrderId, op.ProductId });

    }
  }
}
