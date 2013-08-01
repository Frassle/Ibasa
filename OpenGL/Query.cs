using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ibasa.OpenGL
{
    public sealed class QueryTarget
    {
        uint Target;

        internal QueryTarget(uint target)
        {
            Target = target;
        }

        public Query Query
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetQueryiv(Target, Gl.CURRENT_QUERY, &value);
                    Query query = new Query();
                    query.Id = (uint)value;
                    return query;
                }
            }
        }

        public int Bits
        {
            get
            {
                unsafe
                {
                    int value;
                    Gl.GetQueryiv(Target, Gl.QUERY_COUNTER_BITS, &value);
                    return value;
                }
            }
        }

        public void Begin(Query query)
        {
            Gl.BeginQuery(Target, query.Id);
            GlHelper.GetError();
        }

        public void End()
        {
            Gl.EndQuery(Target);
            GlHelper.GetError();
        }
    }

    public struct Query : IEquatable<Query>
    {
        private static QueryTarget _SamplesPassed = new QueryTarget(Gl.SAMPLES_PASSED);
        public static QueryTarget SamplesPassed { get { return _SamplesPassed; } }
        
        private static QueryTarget _AnySamplesPassed = new QueryTarget(Gl.ANY_SAMPLES_PASSED);
        public static QueryTarget AnySamplesPassed { get { return _AnySamplesPassed; } }

        private static QueryTarget _PrimitivesGenerated = new QueryTarget(Gl.PRIMITIVES_GENERATED);
        public static QueryTarget PrimitivesGenerated { get { return _PrimitivesGenerated; } }

        private static QueryTarget _TransformFeedbackPrimitivesWritten = new QueryTarget(Gl.TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN);
        public static QueryTarget TransformFeedbackPrimitivesWritten { get { return _TransformFeedbackPrimitivesWritten; } }
        
        private static QueryTarget _TimeElapsed = new QueryTarget(Gl.TIME_ELAPSED);
        public static QueryTarget TimeElapsed { get { return _TimeElapsed; } }

        public static readonly Query Null = new Query();

        public uint Id { get; internal set; }

        public Query(uint id)
            : this()
        {
            if (Gl.IsQuery(id) == 0)
                throw new ArgumentException("id is not an OpenGL query object.", "id");
            Id = id;
        }

        public static Query Create()
        {
            unsafe
            {
                uint id;
                Gl.GenQueries(1, &id);
                GlHelper.GetError();
                var query = new Query();
                query.Id = id;
                return query;
            }
        }

        public static void Create(Query[] queries, int index, int count)
        {
            if (queries == null)
                throw new ArgumentNullException("queries");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than 0.");
            if (index + count > queries.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than queries.Length.");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", count, "count is less than 0.");

            unsafe
            {
                uint* ids = stackalloc uint[count];
                Gl.GenQueries(count, ids);
                GlHelper.GetError();
                for (int i = 0; i < count; ++i)
                {
                    var query = new Query();
                    query.Id = ids[i];
                    queries[index + i] = query;
                }
            }
        }

        public void Delete()
        {
            GlHelper.ThrowNullException(Id);
            unsafe
            {
                uint id = Id;
                Gl.DeleteQueries(1, &id);
            }
        }

        public void Counter()
        {
            GlHelper.ThrowNullException(Id);
            Gl.QueryCounter(Id, Gl.TIMESTAMP);
            GlHelper.GetError();
        }

        public string Label
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int length;
                    Gl.GetObjectLabel(Gl.QUERY, Id, 0, &length, null);
                    byte* str = stackalloc byte[length];
                    Gl.GetObjectLabel(Gl.QUERY, Id, length, null, str);

                    int charCount = Encoding.ASCII.GetCharCount(str, length);
                    char[] chars = new char[length];

                    return length == 0
                        ? string.Empty
                        : new string((sbyte*)str, 0, length, Encoding.ASCII);
                }
            }
            set
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int length = Encoding.ASCII.GetByteCount(value);
                    byte* str = stackalloc byte[length];

                    fixed (char* source_ptr = value)
                    {
                        Encoding.ASCII.GetBytes(source_ptr, value.Length, str, length);
                    }

                    Gl.ObjectLabel(Gl.QUERY, Id, length, str);
                    GlHelper.GetError();
                }
            }
        }

        public int Result
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Gl.GetQueryObjectiv(Id, Gl.QUERY_RESULT, &value);
                    return value;
                }
            }
        }

        public bool ResultAvailable
        {
            get
            {
                GlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Gl.GetQueryObjectiv(Id, Gl.QUERY_RESULT_AVAILABLE, &value);
                    return value != 0;
                }
            }
        }

        public override int GetHashCode()
        {
            GlHelper.ThrowNullException(Id);
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GlHelper.ThrowNullException(Id);
            if (obj is Query)
            {
                return Equals((Query)obj);
            }
            return false;
        }

        public bool Equals(Query other)
        {
            GlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(Query left, Query right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Query left, Query right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            GlHelper.ThrowNullException(Id);
            return string.Format("Query: {0}", Id.ToString());
        }
    }
}