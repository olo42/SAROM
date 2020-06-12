using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Olo42.SAROM.WebApp.Logic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using AutoMapper;
using Olo42.SAROM.WebApp.Mappings;
using Olo42.SAROM.Logic.Users;
using Olo42.SFS.Repository.Abstractions;
using Olo42.SFS.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Olo42.SFS.Serialisation.Abstractions;
using Olo42.SFS.Serialisation.Json;
using Olo42.SFS.FileAccess.Abstractions;
using Olo42.SFS.FileAccess.Filesystem;
using Olo42.SAROM.Logic.Operations;

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
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        //app.UseDatabaseErrorPage();
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
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
        {
          endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");
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

      // services.AddScoped<IFileDataAccess<IEnumerable<User>>,
      //   FormatterDataAccess<IEnumerable<User>>>();

      services.AddScoped<ISerialisalizer<IEnumerable<User>>, JsonSerializer<IEnumerable<User>>>();  
      services.AddScoped<IFileAccess, PhysicalFile>();  
      services.AddScoped<IRepository<IEnumerable<User>>, Repository<IEnumerable<User>>>();
      services.AddScoped<IUserManager, UsersManager>();

      services.AddIdentity<User, Role>()
        .AddUserStore<UserStore<User>>()
        .AddRoleStore<RoleStore<Role>>()
        .AddSignInManager<SignInManager<User>>();


      services.AddScoped<ISerialisalizer<Operation>, JsonSerializer<Operation>>();
      services.AddScoped<IRepository<Operation>, Repository<Operation>>();
      


      // services.AddScoped<IFileDataAccess<DataAccess.Contracts.Operation>, FormatterDataAccess<DataAccess.Contracts.Operation>>();
      // services.AddScoped<IFileDataAccess<DataAccess.Contracts.OperationsIndex>, FormatterDataAccess<DataAccess.Contracts.OperationsIndex>>();
      // services.AddScoped<DataAccess.Contracts.IOperationsRepository, OperationsRepository>();

      services.AddAutoMapper(
        typeof(OperationProfile),
        typeof(UserProfile),
        typeof(OperationActionProfile));

      services.AddAuthentication();

      services.AddLocalization(options => options.ResourcesPath = "Resources");

      services.AddControllersWithViews();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
          .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
          .AddDataAnnotationsLocalization();

      // services.AddDbContext<OperationContext>(options => options.UseSqlServer(
      //   Configuration.GetConnectionString("OperationContext")));
    }
  }
}