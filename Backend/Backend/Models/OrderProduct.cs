using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
  public class OrderProduct
  {
    [Key, Required, Column(Order = 1)]
    public int OrderId { get; set; }
    [Key, Required, Column(Order = 2)]
    public int ProductId { get; set; }
    public int Amount { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
  }
}
