using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using XsmService.Utils;

namespace AppUpdaterService
{
    class Program
    {
        public static readonly AppsInformation Ai = new AppsInformation();
        private const string ConfFileName = "AppInfo.json";

        static void Main(string[] args)
        {
            Console.WriteLine("I am live!");
            DirectoryInfo di = new DirectoryInfo(".");
            var dd = di.GetDirectories();
            List<FileInfo> ff = new List<FileInfo>();
            foreach (var d in dd)
            {
                ff.AddRange(d.GetFiles(ConfFileName));
            }

            foreach (var f in ff)
            {
                Ai.Add(f);
            }

            Console.WriteLine("Application count - " + Ai.Count);
            Ai.ConsoleWriteLine();

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
