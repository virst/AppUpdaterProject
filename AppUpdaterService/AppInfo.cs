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

        public class AppFileInfo
        {
            public string FilePath { get; set; }
            public int? ActualRevision;
            public bool Runable { get; set; }
        }
    }
}
