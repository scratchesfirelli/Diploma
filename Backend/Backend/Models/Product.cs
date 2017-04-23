using Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
  public class Product
  {
    public Product() { }

    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    [Required]
    public decimal Price { get; set; }
    public double? Weight { get; set; }
    public double? Width { get; set; }
    public double? Height { get; set; }
    public double? Length { get; set; }

    public int ProductMaterialId { get; set; }
    [ForeignKey("ProductMaterialId")]
    public virtual ProductMaterial ProductMaterial { get; set; }
    public int ProductTypeId { get; set; }
    [ForeignKey("ProductTypeId")]
    public virtual ProductType ProductType { get; set; }
  }
}
