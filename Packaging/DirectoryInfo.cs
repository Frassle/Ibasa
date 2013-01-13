using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ibasa.Packaging
{
    public abstract class DirectoryInfo : FileSystemInfo, IEnumerable<FileSystemInfo>
    {
        public abstract DirectoryInfo Root { get; }
        public abstract DirectoryInfo Parent { get; }

        public abstract void Create();
        public abstract DirectoryInfo CreateSubdirectory(string path);

        public abstract void Delete(bool recursive = false);

        public override void Delete()
        {
            Delete(false);
        }

        public virtual IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern = ".*", System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
        {
            foreach (var info in this)
            {
                var dir = info as DirectoryInfo;
                if (dir == null || !Regex.IsMatch(dir.Name, searchPattern))
                    continue;

                yield return dir;

                if (searchOption == System.IO.SearchOption.AllDirectories)
                {
                    foreach (var child in dir.EnumerateDirectories(searchPattern, searchOption))
                    {
                        yield return child;
                    }
                }
            }
        }
        
        public virtual IEnumerable<FileInfo> EnumerateFiles(string searchPattern = ".*", System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
        {
            foreach (var info in this)
            {
                var dir = info as DirectoryInfo;
                if (dir != null && searchOption == System.IO.SearchOption.AllDirectories)
                {
                    foreach (var child in dir.EnumerateFiles(searchPattern, searchOption))
                    {
                        yield return child;
                    }
                }
                else
                {
                    var file = info as FileInfo;

                    if (file == null || !Regex.IsMatch(file.Name, searchPattern))
                        continue;

                    yield return file;
                }
            }
        }

        public virtual DirectoryInfo[] GetDirectories(string searchPattern = ".*", System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
        {
            return EnumerateDirectories(searchPattern, searchOption).ToArray();
        }

        public virtual FileInfo[] GetFiles(string searchPattern = ".*", System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
        {
            return EnumerateFiles(searchPattern, searchOption).ToArray();
        }

        public abstract IEnumerator<FileSystemInfo> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
