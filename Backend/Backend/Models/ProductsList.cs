using System.Collections.Generic;

namespace Backend.Models
{
  public class ProductsList
  {
    public IEnumerable<Product> Products { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}
