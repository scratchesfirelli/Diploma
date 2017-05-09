using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
  public class User
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }

  }
}
