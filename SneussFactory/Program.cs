using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SneussFactory
{
  public class Program
  {
    public static void Main(atring[] args)
    {
      var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      host.Run();
    }
  }
}