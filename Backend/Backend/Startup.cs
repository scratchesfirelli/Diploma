using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using OrderManagementSystem.Models;
using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Backend
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
      });

      services.AddDbContext<OmsContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      services.AddIdentity<AspNetUser, IdentityRole>().AddEntityFrameworkStores<OmsContext>();
      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
      });

      services.AddTransient<IProductRepository, ProductRepository>();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.AddAuthorization();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseCors("CorsPolicy");
      app.UseIdentity();

      var key = Encoding.UTF8.GetBytes("l40b492eabc2bb8ss02bec8fd5yu3182y74j29090fb3h55b7a0812he1081cf5a6uhl5ed1");

      var options = new JwtBearerOptions
      {
        TokenValidationParameters =
        {
          ValidateIssuer=true,
          ValidateAudience = true,
          ValidIssuer = "MyAuthIssuer",
          ValidAudience = "http://localhost:4200/",
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuerSigningKey = true,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        }
      };

      app.UseJwtBearerAuthentication(options);

      app.UseMvc().UseMvcWithDefaultRoute();
    }
  }
}
