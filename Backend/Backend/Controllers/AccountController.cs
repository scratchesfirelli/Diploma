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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{

  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
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
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor context)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _context = context;
    }

    /// <summary>
    /// POST: api/account/register
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>A result of creating a user</returns>
    [AllowAnonymous, HttpPost("Register")]
    public async Task<JsonResult> Register([FromBody]RegistrationUser model)
    {
      var customerRole = _roleManager.Roles.FirstOrDefault(role => role.Name == "customer");
      var adminRole = _roleManager.Roles.FirstOrDefault(role => role.Name == "admin");
      if (customerRole == null)
      {
        customerRole = new IdentityRole { Name = "customer" };
        await _roleManager.CreateAsync(customerRole);
      }
      if (adminRole == null)
      {
        adminRole = new IdentityRole { Name = "admin" };
        await _roleManager.CreateAsync(adminRole);
      }

      var adminUser = _userManager.Users.FirstOrDefault(user => user.Email == "admin@gmail.com");
      if(adminUser == null)
      {
        adminUser = new User { Email = "admin@gmail.com", UserName = "UltimateAdmin" };
        adminUser.Roles.Add(new IdentityUserRole<string> { RoleId = adminRole.Id, UserId = adminUser.Id });
        await _userManager.CreateAsync(adminUser, "q1w2e3r4");
      }

      User newUser = new User { Email = model.Email, UserName = model.Email };
      newUser.Roles.Add(new IdentityUserRole<string> { RoleId = customerRole.Id, UserId = newUser.Id });
      var res = await _userManager.CreateAsync(newUser, model.Password);
      return new JsonResult(new { success = res.Succeeded, error = String.Join(",", res.Errors.Select(err => err.Description)) }, Settings);
    }

    /// <summary>
    /// GET: api/account/getProfile
    /// ROUTING-TYPE: attribute-based
    /// </summary>
    /// <returns>Returns user profile</returns>
    [Authorize, HttpGet("GetProfile")]
    public async Task<JsonResult> GetProfileAsync()
    {
      var user = await _userManager.FindByEmailAsync(_context.HttpContext.User.Identity.Name);
      return new JsonResult(user, Settings);
    }

    /// <summary>
    /// POST: api/account/login
    /// </summary>
    /// <returns>An error or token if success</returns>
    [AllowAnonymous, HttpPost("Login")]
    public async Task<JsonResult> Login([FromBody]RegistrationUser model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user != null)
      {
        var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (res.Succeeded)
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
          var role = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model.Email));
          return new JsonResult(new { success = true, token = handler.WriteToken(token), role = role.FirstOrDefault() }, Settings);
        }
      }
      return new JsonResult(new { success = false, error = "Wrong login or (and) password" }, Settings);
    }
  }
}
