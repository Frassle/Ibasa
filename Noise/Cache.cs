using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Collections;

namespace Ibasa.Noise
{
    public sealed class Cache<T> : Module<T>
    {
        struct CacheEntry
        {
            public readonly int Dimension;
            public readonly double X;
            public readonly double Y;
            public readonly double Z;
            public readonly double W;
            public readonly double V;
            public readonly double U;
            public readonly T Value;

            public CacheEntry(int dimension, T value, double x, double y = 0.0, double z = 0.0, double w = 0.0, double v = 0.0, double u = 0.0)
            {
                Dimension = dimension;
                Value = value;
                X = x;
                Y = y;
                Z = z;
                W = w;
                V = v;
                U = u;
            }
        }

        Collections.Cache<CacheEntry> CacheBuffer;
        System.Threading.ReaderWriterLockSlim Lock;

        #region Source
        private Module<T> property_Source;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<T> Source
        {
            get { return property_Source; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source = value;
            }
        }
        #endregion

        public Cache(Module<T> source, int cacheSize)
        {
            Source = source;
            CacheBuffer = new Collections.Cache<CacheEntry>(cacheSize);
            Lock = new System.Threading.ReaderWriterLockSlim();
        }

        public override T Evaluate(double x)
        {
            Lock.EnterReadLock();
            foreach (var entry in CacheBuffer)
            {
                if (entry.Dimension == 1
                    && entry.X == x)
                {
                    Lock.ExitReadLock();
                    return entry.Value;
                }
            }
            Lock.ExitReadLock();

            Lock.EnterWriteLock();
            T value = Source.Evaluate(x);
            CacheBuffer.AddLast(new CacheEntry(1, value, x));
            Lock.ExitWriteLock();
            return value;
        }

        public override T Evaluate(double x, double y)
        {
            Lock.EnterReadLock();
            foreach (var entry in CacheBuffer)
            {
                if (entry.Dimension == 2
                    && entry.X == x && entry.Y == y)
                {
                    Lock.ExitReadLock();
                    return entry.Value;
                }
            }
            Lock.ExitReadLock();

            Lock.EnterWriteLock();
            T value = Source.Evaluate(x, y);
            CacheBuffer.AddLast(new CacheEntry(2, value, x, y));
            Lock.ExitWriteLock();
            return value;
        }

        public override T Evaluate(double x, double y, double z)
        {
            Lock.EnterReadLock();
            foreach (var entry in CacheBuffer)
            {
                if (entry.Dimension == 3
                    && entry.X == x && entry.Y == y
                    && entry.Z == z)
                {
                    Lock.ExitReadLock();
                    return entry.Value;
                }
            }
            Lock.ExitReadLock();

            Lock.EnterWriteLock();
            T value = Source.Evaluate(x, y, z);
            CacheBuffer.AddLast(new CacheEntry(3, value, x, y, z));
            Lock.ExitWriteLock();
            return value;
        }

        public override T Evaluate(double x, double y, double z, double w)
        {
            Lock.EnterReadLock();
            foreach (var entry in CacheBuffer)
            {
                if (entry.Dimension == 4
                    && entry.X == x && entry.Y == y
                    && entry.Z == z && entry.W == w)
                {
                    Lock.ExitReadLock();
                    return entry.Value;
                }
            }
            Lock.ExitReadLock();

            Lock.EnterWriteLock();
            T value = Source.Evaluate(x, y, z, w);
            CacheBuffer.AddLast(new CacheEntry(4, value, x, y, z, w));
            Lock.ExitWriteLock();
            return value;
        }

        public override T Evaluate(double x, double y, double z, double w, double v, double u)
        {
            Lock.EnterReadLock();
            foreach (var entry in CacheBuffer)
            {
                if (entry.Dimension == 6
                    && entry.X == x && entry.Y == y
                    && entry.Z == z && entry.W == w
                    && entry.V == v && entry.U == u)
                {
                    Lock.ExitReadLock();
                    return entry.Value;
                }
            }
            Lock.ExitReadLock();

            Lock.EnterWriteLock();
            T value = Source.Evaluate(x, y, z, w, v, u);
            CacheBuffer.AddLast(new CacheEntry(6, value, x, y, z, w, v, u));
            Lock.ExitWriteLock();
            return value;
        }
    }
}
