using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace Ibasa.Audio
{
    internal static class ObjectTable
    {
        private static ConcurrentDictionary<int, ALObject> Objects = new ConcurrentDictionary<int, ALObject>();

        public static ALObject Get(int id)
        {
            return Objects[id];
        }

        public static Buffer GetBuffer(int id)
        {
            var obj = Objects[id];
            if (obj.IsBuffer)
                return obj as Buffer;
            else
                throw new InvalidCastException(string.Format("{0} is not a buffer.", id));
        }

        public static Source GetSource(int id)
        {
            var obj = Objects[id];
            if (obj.IsSource)
                return obj as Source;
            else
                throw new InvalidCastException(string.Format("{0} is not a source.", id));
        }

        public static void Add(ALObject obj)
        {
            Objects.TryAdd(obj.Id, obj);
        }

        public static void Remove(ALObject obj)
        {
            Objects.TryRemove(obj.Id, out obj);
        }
    }
}
