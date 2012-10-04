using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ibasa.Packaging
{
    public abstract class FileInfo : FileSystemInfo
    {
        public abstract long Length { get; }

        public abstract DirectoryInfo Directory { get; }

        public abstract Stream Create();

        public virtual Stream Open(FileMode mode)
        {
            return Open(mode, FileAccess.ReadWrite, FileShare.None);
        }
        public virtual Stream Open(FileMode mode, FileAccess access)
        {
            return Open(mode, access, FileShare.None);
        }
        public abstract Stream Open(FileMode mode, FileAccess access, FileShare share);

        public virtual Stream OpenRead()
        {
            return Open(FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        public virtual Stream OpenWrite()
        {
            return Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        }

        public virtual StreamReader OpenText()
        {
            return new StreamReader(Open(FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.UTF8);
        }
        public virtual StreamWriter AppendText()
        {
            return new StreamWriter(Open(FileMode.Append, FileAccess.Write, FileShare.None), Encoding.UTF8);
        }
        public virtual StreamWriter CreateText()
        {
            return new StreamWriter(Open(FileMode.Create, FileAccess.Write, FileShare.None), Encoding.UTF8);
        }
    }
}
