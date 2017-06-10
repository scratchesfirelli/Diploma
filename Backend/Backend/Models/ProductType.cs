using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
  public class ProductType
  {
    public ProductType()
    {
      Product = new List<Product>();
    }
    [Key]
    public int Id { get; set; }
    [MaxLength(75), Required]
    public string Title { get; set; }
    public virtual ICollection<Product> Product { get; set; }
  }
}
