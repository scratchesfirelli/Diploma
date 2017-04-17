﻿using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Models
{
  public class OmsContext : DbContext
  {
    public OmsContext(DbContextOptions<OmsContext> options)
            : base(options)
        { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMaterial> ProductMaterials { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
  }
}
