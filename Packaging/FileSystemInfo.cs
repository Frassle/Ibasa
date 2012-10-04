using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Packaging
{
    public abstract class FileSystemInfo
    {
        public abstract string Name { get; }
        public abstract string FullName { get; }
        public abstract string Extension { get; }
        public abstract bool Exists { get; }

        public abstract void Delete();
    }
}
