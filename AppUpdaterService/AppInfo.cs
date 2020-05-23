using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
// ReSharper disable UnusedMember.Global
// ReSharper disable once IdentifierTypo

namespace AppUpdaterService
{
    public class AppInfo
    {
        public string AppName { get; set; }
        private List<AppFileInfo> _files = new List<AppFileInfo>();
        public List<AppFileInfo> Files => _files;
        public char Splitter { get; set; } = Path.DirectorySeparatorChar;
        public string DirectoryPath;
        public Rev MaxRev;

        public string GetFilePath(AppFileInfo f)
        {
            string s = Path.Combine(DirectoryPath, MaxRev.S);
            return  Path.Combine(s, f.FilePath.TrimStart('.').TrimStart(Splitter));
        }

        public string GetFilePath(int n)
        {
            string s = Path.Combine(DirectoryPath, Files[n].ActualRevision.S);
            return Path.Combine(s, Files[n].FilePath.TrimStart('.').TrimStart(Splitter));
        }

        public class AppFileInfo
        {
            public string FilePath { get; set; }
            public Rev ActualRevision;
            public bool Runable { get; set; }
        }

        public class Rev
        {
            public Rev(int n,string s)
            {
                S = s;
                N = n;
            }

            public readonly int N;
            public readonly string S;
        }
    }
}
