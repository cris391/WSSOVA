using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using DatabaseService;

namespace WebApi
{
  public class Program
  {
    public static User CurrentUser = null;
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
