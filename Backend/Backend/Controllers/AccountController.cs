using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Backend.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{

  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly UserManager<AspNetUser> _userManager;
    private readonly SignInManager<AspNetUser> _signInManager;
    private readonly IHttpContextAccessor _context;
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
    public AccountController(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager, IHttpContextAccessor context)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _context = context;
    }

    /// <summary>
    /// POST: api/account/register
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>A result of creating a user</returns>
    [AllowAnonymous, HttpPost("Register")]
    public async Task<JsonResult> Register([FromBody]User model)
    {
      AspNetUser user = new AspNetUser { Email = model.Email, UserName = model.Email };
      var res = await _userManager.CreateAsync(user, model.Password);
      return new JsonResult(new { success = res.Succeeded, error = String.Join(",", res.Errors.Select(err => err.Description)) }, Settings);
    }

    /// <summary>
    /// GET: api/account/getProfile
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>Returns user profile</returns>
    [Authorize, HttpGet("GetProfile")]
    public JsonResult GetProfile()
    {
      var identity = _context.HttpContext.User.Identity.Name;
      return new JsonResult(new { user = new { email = _context.HttpContext.User.Identity.Name } }, Settings);
    }

    /// <summary>
    /// POST: api/account/register
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>A result of creating a user</returns>
    [AllowAnonymous, HttpPost("Login")]
    public async Task<JsonResult> Login([FromBody]User model)
    {
      AspNetUser user = new AspNetUser { Email = model.Email, UserName = model.Email };
      var res = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
      if(res.Succeeded)
      {
        var handler = new JwtSecurityTokenHandler();
        var sec = "l40b492eabc2bb8ss02bec8fd5yu3182y74j29090fb3h55b7a0812he1081cf5a6uhl5ed1";
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sec));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var identity = new ClaimsIdentity(new GenericIdentity(model.Email), _context.HttpContext.User.Claims);
        var token = handler.CreateJwtSecurityToken(subject: identity,
                                                   signingCredentials: signingCredentials,
                                                   audience: "http://localhost:4200/",
                                                   issuer: "MyAuthIssuer",
                                                   expires: DateTime.UtcNow.AddDays(3));
        return new JsonResult(new { success = true, token = handler.WriteToken(token) }, Settings);
      }
      return new JsonResult(new { success = false, error = "Wrong login or (and) password" }, Settings);
    }
  }
}
