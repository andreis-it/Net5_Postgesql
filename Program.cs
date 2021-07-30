using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Net5Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception: " + Environment.NewLine + ex.ToString());
                Console.ReadLine();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                        webBuilder.UseIISIntegration();
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
