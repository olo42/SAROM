using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Olo42.SAROM.DataAccess;
using Olo42.SAROM.DataAccess.Contracts;
using Olo42.SAROM.WebApp.Logic;
using Olo42.SAROM.WebApp.Models;
using Olo42.FileDataAccess.Contracts;
using Olo42.FileDataAccess;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using AutoMapper;
using Olo42.SAROM.WebApp.Mappings;

namespace Olo42.SAROM.WebApp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      var supportedCultures = new[]
      {
          new CultureInfo("de"),
          new CultureInfo("en"),
      };

      app.UseRequestLocalization(new RequestLocalizationOptions
      {
        DefaultRequestCulture = new RequestCulture("de"),
        // Formatting numbers, dates, etc.
        SupportedCultures = supportedCultures,
        // UI strings that we have localized.
        SupportedUICultures = supportedCultures
      });

      //app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddScoped<IFormatter, BinaryFormatter>();

      services.AddScoped<
        IFileDataAccess<IEnumerable<User>>, 
        FormatterDataAccess<IEnumerable<User>>>();

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<ISaromUserStore<User>, UserStore<User>>();

      services.AddIdentity<User, Role>()
        .AddUserStore<UserStore<User>>()
        .AddRoleStore<RoleStore<Role>>()
        .AddSignInManager<SignInManager<User>>();

      services.AddScoped<IFileDataAccess<DataAccess.Contracts.Operation>, FormatterDataAccess<DataAccess.Contracts.Operation>>();
      services.AddScoped<IFileDataAccess<OperationsIndex>, FormatterDataAccess<OperationsIndex>>();
      services.AddScoped<IOperationsRepository, OperationsRepository>();

      services.AddAutoMapper(
        typeof(OperationProfile),
        typeof(UnitProfile),
        typeof(OperationActionProfile));

      services.AddAuthentication();

      services.AddLocalization(options => options.ResourcesPath = "Resources");

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
          .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
          .AddDataAnnotationsLocalization();

      // services.AddDbContext<OperationContext>(options => options.UseSqlServer(
      //   Configuration.GetConnectionString("OperationContext")));
    }
  }
}