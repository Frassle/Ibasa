using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Packaging
{
    public abstract class Package
    {
        public abstract DirectoryInfo Root { get; }

        public abstract Path Path { get; }

        public abstract DirectoryInfo GetDirectoryInfo(string path);
        public abstract FileInfo GetFileInfo(string path);
    }
}
