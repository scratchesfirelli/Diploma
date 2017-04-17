using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
  public class ProductMaterial
  {
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public virtual Product Product { get; set; }
  }
}
