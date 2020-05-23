using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using XsmService.Utils;
using static AppUpdaterService.AppInfo;

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
            foreach(KeyValuePair<string, AppInfo> a in Ai)
            {
                #region Получаем все ревизии
                di = new DirectoryInfo(a.Value.DirectoryPath);
                var revFolders = di.GetDirectories("r*");
                int r;
                Dictionary<int, string> revs = new Dictionary<int, string>();
                foreach(var rf in revFolders)
                {
                    if (int.TryParse(rf.Name.TrimStart('r'), out r))
                        revs[r] = rf.Name;
                }
                List<Rev> revsList = new List<Rev>();
                foreach (var rv in revs)
                    revsList.Add(new Rev(rv.Key, rv.Value));
                revsList = revsList.OrderBy(t => t.N).ToList();
                #endregion
                #region Расставляем ревизии по файлам
                foreach (var rv in revsList)
                {
                    a.Value.MaxRev = rv;
                    foreach(var f in a.Value.Files)
                    {
                        if (File.Exists(a.Value.GetFilePath(f)))
                            f.ActualRevision = rv;
                    }
                }
                #endregion
            }
            Ai.ConsoleWriteLine();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
                  // webBuilder.UseUrls("https://localhost:5001/");
               });
    }
}
