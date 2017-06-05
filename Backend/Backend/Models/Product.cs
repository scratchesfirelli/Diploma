using Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
  public class Product
  {
    public Product() { }

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(75)]
    public string Title { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    [Required, DataType(DataType.Currency)]
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
