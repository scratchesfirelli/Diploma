using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Backend.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{

  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly UserManager<AspNetUser> _userManager;
    private readonly SignInManager<AspNetUser> _signInManager;
    public JsonSerializerSettings Settings
    {
      get
      {
        return new JsonSerializerSettings()
        {
          Formatting = Formatting.Indented
        };
      }
    }
    public AccountController(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    /// <summary>
    /// POST: api/account/register
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>A result of creating a user</returns>
    [HttpPost("Register")]
    public async Task<JsonResult> Register([FromBody]User model)
    {
      AspNetUser user = new AspNetUser { Email = model.Email, UserName = model.Email };
      var res = await _userManager.CreateAsync(user, model.Password);
      return new JsonResult(new { success = res.Succeeded, error = String.Join(",", res.Errors.Select(err => err.Description)) }, Settings);
    }

    /// <summary>
    /// POST: api/account/register
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>A result of creating a user</returns>
    [HttpPost("Login")]
    public async Task<JsonResult> Login([FromBody]User model)
    {
      AspNetUser user = new AspNetUser { Email = model.Email, UserName = model.Email };
      var res = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
      return new JsonResult(new { success = res.Succeeded, error = res.Succeeded ? null : "Wrong login or (and) password" }, Settings);
    }
  }
}
