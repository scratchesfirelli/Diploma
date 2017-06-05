﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
  public class ProductMaterial
  {
    [Key]
    public int Id { get; set; }
    [MaxLength(75), Required]
    public string Title { get; set; }
    public virtual Product Product { get; set; }
  }
}
