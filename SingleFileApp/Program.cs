using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace SingleFileApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = Path.Combine(Path.GetTempPath(), ".net", typeof(Program).Assembly.GetName().Name);

            var directory = 
                Directory
                    .GetDirectories(path)
                    .Select(path => new DirectoryInfo(path))
                    .OrderByDescending(di => di.LastWriteTime)
                    .First();

            CreateHostBuilder(args)
                .UseContentRoot(directory.FullName)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)            
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
