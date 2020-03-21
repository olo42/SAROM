using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Olo42.SAROM.WebApp
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var isService = !(Debugger.IsAttached || args.Contains("--console"));
      var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

      if (isService)
      {
        var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
        var pathToContentRoot = Path.GetDirectoryName(pathToExe);
        builder.UseContentRoot(pathToContentRoot);
      }

      var host = builder.Build();

      if (isService)
      {
        host.RunAsService();
      }
      else
      {
        host.Run();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
          //.ConfigureAppConfiguration((hostingContext, config) =>
          //{
          //  config.AddJsonFile(
          //      "config.json", optional: true, reloadOnChange: true);
          //})
          .UseStartup<Startup>();
  }
}