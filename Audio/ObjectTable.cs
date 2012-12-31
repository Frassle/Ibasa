using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    internal static class ObjectTable
    {
        private static Dictionary<int, ALObject> Objects = new Dictionary<int, ALObject>();

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
            Objects.Add(obj.Id, obj);
        }

        public static void Remove(ALObject obj)
        {
            Objects.Remove(obj.Id);
        }
    }
}
