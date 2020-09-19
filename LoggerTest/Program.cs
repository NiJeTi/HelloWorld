using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LoggerTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Host
               .CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
               .Build()
               .Run();
        }
    }
}