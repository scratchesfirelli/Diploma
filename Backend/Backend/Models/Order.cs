using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
  public class Order
  {
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    [Required]
    public DateTime AddDate { get; set; }
    public DateTime CompleteDate { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
  }
}
