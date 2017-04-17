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
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    [Required]
    public decimal Price { get; set; }
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }

    [ForeignKey("ProductMaterial")]
    public Guid ProductMaterialId { get; set; }
    [Required, ForeignKey("ProductType")]
    public Guid ProductTypeId { get; set; }
  }
}
