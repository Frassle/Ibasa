using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Packaging
{
    public sealed class FileSystemPackage : Package
    {
        sealed class FileSystemDirectoryInfo : DirectoryInfo
        {
            System.IO.DirectoryInfo Info;

            public FileSystemDirectoryInfo(string path)
            {
                Info = new System.IO.DirectoryInfo(path);
            }

            public FileSystemDirectoryInfo(System.IO.DirectoryInfo info)
            {
                Info = info;
            }

            public override DirectoryInfo Root
            {
                get { return new FileSystemDirectoryInfo(Info.Root); }
            }

            public override DirectoryInfo Parent
            {
                get { return new FileSystemDirectoryInfo(Info.Parent); }
            }

            public override void Create()
            {
                Info.Create();
            }

            public override DirectoryInfo CreateSubdirectory(string path)
            {
                return new FileSystemDirectoryInfo(Info.CreateSubdirectory(path));
            }

            public override void Delete(bool recursive = false)
            {
                Info.Delete(recursive);
            }

            public override IEnumerator<FileSystemInfo> GetEnumerator()
            {
                foreach (var item in Info.EnumerateFileSystemInfos())
                {
                    if (item is System.IO.FileInfo)
                        yield return new FileSystemFileInfo(item as System.IO.FileInfo);

                    if (item is System.IO.DirectoryInfo)
                        yield return new FileSystemDirectoryInfo(item as System.IO.DirectoryInfo);
                }
            }

            public override string Name
            {
                get { return Info.Name; }
            }

            public override string FullName
            {
                get { return Info.FullName; }
            }

            public override string Extension
            {
                get { return Info.Extension; }
            }

            public override bool Exists
            {
                get { return Info.Exists; }
            }
        }

        sealed class FileSystemFileInfo : FileInfo
        {
            System.IO.FileInfo Info;

            public FileSystemFileInfo(string path)
            {
                Info = new System.IO.FileInfo(path);
            }

            public FileSystemFileInfo(System.IO.FileInfo info)
            {
                Info = info;
            }

            public override long Length
            {
                get { return Info.Length; }
            }

            public override DirectoryInfo Directory
            {
                get { return new FileSystemDirectoryInfo(Info.Directory); }
            }

            public override System.IO.Stream Create()
            {
                return Info.Create();
            }

            public override System.IO.Stream Open(System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share)
            {
                return Info.Open(mode, access, share);
            }

            public override string Name
            {
                get { return Info.Name; }
            }

            public override string FullName
            {
                get { return Info.FullName; }
            }

            public override string Extension
            {
                get { return Info.Extension; }
            }

            public override bool Exists
            {
                get { return Info.Exists; }
            }

            public override void Delete()
            {
                Info.Delete();
            }
        }

        sealed class FileSystemPath : Path
        {
            public override string ChangeExtension(string path, string extension)
            {
                return System.IO.Path.ChangeExtension(path, extension);
            }

            public override string Combine(params string[] paths)
            {
                return System.IO.Path.Combine(paths);
            }

            public override char[] DirectorySeparatorChars
            {
                get
                {
                    return new char[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar };
                }
            }

            public override string GetDirectoryName(string path)
            {
                return System.IO.Path.GetDirectoryName(path);
            }

            public override string GetExtension(string path)
            {
                return System.IO.Path.GetExtension(path);
            }

            public override string GetFileName(string path)
            {
                return System.IO.Path.GetFileName(path);
            }

            public override string GetFileNameWithoutExtension(string path)
            {
                return System.IO.Path.GetFileNameWithoutExtension(path);
            }

            public override string GetFullPath(string path)
            {
                return System.IO.Path.GetFullPath(path);
            }

            public override string GetPathRoot(string path)
            {
                return System.IO.Path.GetPathRoot(path);
            }
            
            public override bool HasExtension(string path)
            {
                return System.IO.Path.HasExtension(path);
            }

            public override bool IsPathRooted(string path)
            {
                return System.IO.Path.IsPathRooted(path);
            }
        }

        public FileSystemPackage(string root)
        {
            _Root = root;
        }

        string _Root;

        public override DirectoryInfo Root
        {
            get { return new FileSystemDirectoryInfo(_Root); }
        }

        public override Path Path
        {
            get { return new FileSystemPath(); }
        }

        public override DirectoryInfo GetDirectoryInfo(string path)
        {
            return new FileSystemDirectoryInfo(path);
        }

        public override FileInfo GetFileInfo(string path)
        {
            return new FileSystemFileInfo(path);
        }
    }
}
