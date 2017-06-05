using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
  public class User : IdentityUser
  {
    [MaxLength(256)]
    public string Address { get; set; }
    [MaxLength(256)]
    public string FullName { get; set; }

  }
}
