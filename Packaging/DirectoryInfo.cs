using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Packaging
{
    public abstract class DirectoryInfo : FileSystemInfo, IEnumerable<FileSystemInfo>
    {
        public abstract DirectoryInfo Root { get; }
        public abstract DirectoryInfo Parent { get; }

        public abstract void Create();
        public abstract DirectoryInfo CreateSubdirectory(string path);

        public override void Delete()
        {
 	        Delete(false);
        }
        public abstract void Delete(bool recursive);

        private static bool Match(string pattern, string text, int patternIndex, int textIndex)
        {
            if (patternIndex == pattern.Length || textIndex == text.Length)
                return patternIndex == pattern.Length && textIndex == text.Length;

            switch (pattern[patternIndex])
            {
                case '*':
                    return Match(pattern, text, patternIndex + 1, textIndex) ||
                        textIndex != text.Length && Match(pattern, text, patternIndex, textIndex + 1);
                case '?':
                    return textIndex != text.Length && Match(pattern, text, patternIndex + 1, textIndex + 1);
                default:
                    return (pattern[patternIndex] == text[textIndex])
                        && Match(pattern, text, patternIndex + 1, textIndex + 1);
            }
        }

        private static bool Match(string pattern, string text)
        {
            return Match(pattern, text, 0, 0);
        }

        public virtual IEnumerable<DirectoryInfo> EnumerateDirectories()
        {
            return EnumerateDirectories("*", System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
        {
            return EnumerateDirectories(searchPattern, System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, System.IO.SearchOption searchOption)
        {
            foreach (var info in this)
            {
                var dir = info as DirectoryInfo;
                if (dir == null || !Match(searchPattern, dir.Name))
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

        public virtual IEnumerable<FileInfo> EnumerateFiles()
        {
            return EnumerateFiles("*", System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
        {
            return EnumerateFiles(searchPattern, System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual IEnumerable<FileInfo> EnumerateFiles(string searchPattern, System.IO.SearchOption searchOption)
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

                    if (file == null || !Match(searchPattern, file.Name))
                        continue;

                    yield return file;
                }
            }
        }

        public virtual DirectoryInfo[] GetDirectories()
        {
            return GetDirectories("*", System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual DirectoryInfo[] GetDirectories(string searchPattern)
        {
            return GetDirectories(searchPattern, System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual DirectoryInfo[] GetDirectories(string searchPattern, System.IO.SearchOption searchOption)
        {
            return EnumerateDirectories(searchPattern, searchOption).ToArray();
        }

        public virtual FileInfo[] GetFiles()
        {
            return GetFiles("*", System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual FileInfo[] GetFiles(string searchPattern)
        {
            return GetFiles(searchPattern, System.IO.SearchOption.TopDirectoryOnly);
        }
        public virtual FileInfo[] GetFiles(string searchPattern, System.IO.SearchOption searchOption)
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
