using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XsmService.Utils;

namespace AppUpdaterService
{
    public class AppsInformation : Dictionary<string, AppInfo>
    {
        public AppsInformation() : base(StringComparer.CurrentCultureIgnoreCase)
        {

        }

        public string Add(FileInfo f)
        {
            var s = File.ReadAllText(f.FullName, Encoding.UTF8);
            var a = JsonUtil<AppInfo>.ObjFromStr(s);
            this[a.AppName] = a;
            a.DirectoryPath = f.Directory?.FullName;
            return a.AppName;
        }


        public void ConsoleWriteLine()
        {
            foreach (var t in this)
            {
                Console.WriteLine("{0} - {1}({2})", t.Key, t.Value.AppName, t.Value.DirectoryPath);
            }
        }
    }
}
