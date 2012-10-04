using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Ibasa.Packaging
{
    public class Path
    {
        public virtual char[] DirectorySeparatorChars { get { return new char[] { '\\', '/' }; } }

        public virtual string Combine(params string[] paths)
        {
            if (paths == null)
                throw new ArgumentNullException("paths");

            int capacity = 0;
            int index = 0;
            for (int i = 0; i < paths.Length; ++i)
            {
                if (paths[i] == null)
                    throw new ArgumentNullException("paths");

                if (IsPathRooted(paths[i]))
                {
                    index = i;
                    capacity = paths[i].Length;
                }
                else
                {
                    capacity += paths[i].Length;
                }

                if (DirectorySeparatorChars.Any(c => c == paths[i].Last()))
                {
                    capacity++;
                }
            }

            StringBuilder builder = new StringBuilder(capacity);
            for (int i = index; i < paths.Length; ++i)
            {
                if (paths[i].Length != 0)
                {
                    if (builder.Length == 0)
                    {
                        builder.Append(paths[i]);
                    }
                    else
                    {
                        char lastChar = builder[builder.Length - 1];
                        if (DirectorySeparatorChars.Any(c => c == lastChar))
                        {
                            builder.Append(DirectorySeparatorChars[0]);
                        }
                        builder.Append(paths[i]);
                    }
                }
            }
            return builder.ToString();
        }

        public virtual string ChangeExtension(string path, string extension)
        {
            if (path == null)
                return null;

            int length = path.Length;
            string str = path;
            while (--length >= 0)
            {
                if (path[length] == '.')
                {
                    str = path.Substring(0, length);
                    break;
                }
                if (DirectorySeparatorChars.Any(c => c == path[length]))
                    break;
            }

            if (extension == null || path.Length == 0)
                return str;
            if (extension.Length == 0 || extension[0] != '.')
                str = str + '.';
            return str + extension;
        }

        public virtual string GetExtension(string path)
        {
            if (path == null)
                return null;

            int length = path.Length;
            while (--length >= 0)
            {
                if (path[length] == '.')
                {
                    return path.Substring(length);
                }
                if (DirectorySeparatorChars.Any(c => c == path[length]))
                    break;
            }

            return String.Empty;
        }
        public virtual string GetFileName(string path)
        {
            if (path != null)
            {
                int length = path.Length;
                while (--length >= 0)
                {
                    if (DirectorySeparatorChars.Any(c => c == path[length]))
                    {
                        return path.Substring(length + 1);
                    }
                }
            }
            return path;
        }
        public virtual string GetDirectoryName(string path)
        {
            string filename = GetFileName(path);
            return path.Substring(0, path.Length - filename.Length);
        }
        public virtual string GetFullPath(string path)
        {
            if (IsPathRooted(path))
                return path;
            else
                return Combine(DirectorySeparatorChars[0].ToString(), path);
        }
        public virtual string GetPathRoot(string path)
        {
            if (path == null)
                return null;

            if (DirectorySeparatorChars.Any(c => c == path[0]))
                return path[0].ToString();
            else
                return string.Empty;
        }
        public virtual string GetFileNameWithoutExtension(string path)
        {
            string filename = GetFileName(path);
            return filename.Substring(0, filename.Length - GetExtension(filename).Length);
        }

        public virtual bool HasExtension(string path)
        {
            if (path != null)
            {
                int length = path.Length;
                while (--length >= 0)
                {
                    if (path[length] == '.')
                    {
                        return true;
                    }
                    if (DirectorySeparatorChars.Any(c => c == path[length]))
                        break;
                }
            }
            return false;
        }
        public virtual bool IsPathRooted(string path)
        {
            if (path == null)
                return false;

            if (path.Length == 0)
                return false;

            return DirectorySeparatorChars.Any(c => c == path[0]);
        }
    }
}
