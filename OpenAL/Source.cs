using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Ibasa.Numerics.Geometry;

namespace Ibasa.OpenAL
{

    public struct Source : IEquatable<Source>
    {
        public static readonly Source Null = new Source();

        public uint Id { get; private set; }

        public Source(uint id)
            : this()
        {
            if (Al.IsSource(id) == 0)
                throw new ArgumentException("id is not an OpenAL source identifier.", "id");

            Id = id;
        }

        public static Source Create()
        {
            unsafe
            {
                uint id;
                Al.GenSources(1, &id);
                AlHelper.GetAlError(Al.GetError());
                AlHelper.CheckName(id, "source");
                return new Source(id);
            }
        }

        public static void Create(Source[] sources, int index, int count)
        {
            if (sources == null)
                throw new ArgumentNullException("sources");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", index, "index is less than 0.");
            if (index + count > sources.Length)
                throw new ArgumentOutOfRangeException("count", count, "index + count is greater than buffers.Length.");

            unsafe
            {
                uint* ids = stackalloc uint[count];
                Al.GenSources(count, ids);
                AlHelper.GetAlError(Al.GetError());
                for (int i = 0; i < count; ++i)
                {
                    AlHelper.CheckName(ids[i], "source");
                    sources[index + i] = new Source(ids[i]);
                }
            }
        }

        public void Delete()
        {
            AlHelper.ThrowNullException(Id);
            unsafe
            {
                uint id = Id;
                Al.DeleteSources(1, &id);
            }
        }

        public SourceType Type
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetSourcei(Id, Al.SOURCE_TYPE, &value);
                    return (SourceType)value;
                }
            }
        }

        public SourceState State
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetSourcei(Id, Al.SOURCE_STATE, &value);
                    return (SourceState)value;
                }
            }
        }

        public int ByteOffset
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetSourcei(Id, Al.BYTE_OFFSET, &value);
                    return value;
                }
            }
            set
            {
                AlHelper.ThrowNullException(Id);
                Al.Sourcei(Id, Al.BYTE_OFFSET, value);
            }
        }

        public int SampleOffset
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetSourcei(Id, Al.SAMPLE_OFFSET, &value);
                    return value;
                }
            }
            set
            {
                AlHelper.ThrowNullException(Id);
                Al.Sourcei(Id, Al.SAMPLE_OFFSET, value);
            }
        }

        public int BuffersProcessed
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetSourcei(Id, Al.BUFFERS_PROCESSED, &value);
                    return value;
                }
            }
        }

        public int BuffersQueued
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int value;
                    Al.GetSourcei(Id, Al.BUFFERS_QUEUED, &value);
                    return value;
                }
            }
        }

        public Buffer Buffer
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int id;
                    Al.GetSourcei(Id, Al.BUFFER, &id);
                    return new Buffer((uint)id);
                }
            }
            set
            {
                AlHelper.ThrowNullException(Id);
                Al.Sourcei(Id, Al.BUFFER, (int)value.Id);
            }
        }

        public bool Looping
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int id;
                    Al.GetSourcei(Id, Al.LOOPING, &id);
                    return id != 0;
                }
            }
            set
            {
                AlHelper.ThrowNullException(Id);
                Al.Sourcei(Id, Al.LOOPING, value ? 1 : 0);
            }
        }

        public bool SourceRelative
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    int id;
                    Al.GetSourcei(Id, Al.SOURCE_RELATIVE, &id);
                    return id != 0;
                }
            }
            set
            {
                AlHelper.ThrowNullException(Id);
                Al.Sourcei(Id, Al.SOURCE_RELATIVE, value ? 1 : 0);
            }
        }

        public Vector3f Direction
        {
            get
            {
                unsafe
                {
                    float x, y, z;
                    Al.GetSource3f(Id, Al.DIRECTION, &x, &y, &z);
                    return new Vector3f(x, y, z);
                }
            }
            set
            {
                Al.Source3f(Id, Al.DIRECTION, value.X, value.Y, value.Z);
            }
        }

        public Point3f Position
        {
            get
            {
                unsafe
                {
                    float x, y, z;
                    Al.GetSource3f(Id, Al.POSITION, &x, &y, &z);
                    return new Point3f(x, y, z);
                }
            }
            set
            {
                Al.Source3f(Id, Al.POSITION, value.X, value.Y, value.Z);
            }
        }

        public Vector3f Velocity
        {
            get
            {
                unsafe
                {
                    float x, y, z;
                    Al.GetSource3f(Id, Al.VELOCITY, &x, &y, &z);
                    return new Vector3f(x, y, z);
                }
            }
            set
            {
                Al.Source3f(Id, Al.VELOCITY, value.X, value.Y, value.Z);
            }
        }

        public float ConeInnerAngle
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.CONE_INNER_ANGLE, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.CONE_INNER_ANGLE, value);
            }
        }

        public float ConeOuterAngle
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.CONE_OUTER_ANGLE, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.CONE_OUTER_ANGLE, value);
            }
        }

        public float ConeOuterGain
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.CONE_OUTER_GAIN, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.CONE_OUTER_GAIN, value);
            }
        }

        public float Gain
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.GAIN, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.GAIN, value);
            }
        }

        public float MaxDistance
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.MAX_DISTANCE, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.MAX_DISTANCE, value);
            }
        }

        public float MaxGain
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.MAX_GAIN, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.MAX_GAIN, value);
            }
        }

        public float MinGain
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.MIN_GAIN, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.MIN_GAIN, value);
            }
        }

        public float Pitch
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.PITCH, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.PITCH, value);
            }
        }

        public float ReferenceDistance
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.REFERENCE_DISTANCE, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.REFERENCE_DISTANCE, value);
            }
        }

        public float RolloffFactor
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.ROLLOFF_FACTOR, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.ROLLOFF_FACTOR, value);
            }
        }

        public float SecOffset
        {
            get
            {
                AlHelper.ThrowNullException(Id);
                unsafe
                {
                    float value;
                    Al.GetSourcef(Id, Al.SEC_OFFSET, &value);
                    return value;
                }
            }
            set
            {
                Al.Sourcef(Id, Al.SEC_OFFSET, value);
            }
        }

        public void Pause()
        {
            AlHelper.ThrowNullException(Id);
            Al.SourcePause(Id);
        }

        public void Play()
        {
            AlHelper.ThrowNullException(Id);
            Al.SourcePlay(Id);
        }

        public void Rewind()
        {
            AlHelper.ThrowNullException(Id);
            Al.SourceRewind(Id);
        }

        public void Stop()
        {
            AlHelper.ThrowNullException(Id);
            Al.SourceStop(Id);
        }

        public void Queue(Buffer buffer)
        {
            AlHelper.ThrowNullException(Id);
            if (buffer == Buffer.Null)
                throw new ArgumentNullException("buffer");

            unsafe
            {
                uint bid = buffer.Id;
                Al.SourceQueueBuffers(Id, 1, &bid);
            }
        }

        public void Queue(Buffer[] buffers)
        {
            AlHelper.ThrowNullException(Id);
            if (buffers == null)
                throw new ArgumentNullException("buffers");

            unsafe
            {
                uint* bids = stackalloc uint[buffers.Length];
                for (int i = 0; i < buffers.Length; ++i)
                {
                    bids[i] = buffers[i].Id;
                }
                Al.SourceQueueBuffers(Id, buffers.Length, bids);
            }
        }

        public Buffer Unqueue()
        {
            AlHelper.ThrowNullException(Id);

            unsafe
            {
                uint bid;
                Al.SourceUnqueueBuffers(Id, 1, &bid);
                return new OpenAL.Buffer(bid);
            }
        }

        public Buffer[] Unqueue(int count)
        {
            AlHelper.ThrowNullException(Id);
            if (count <= 0)
                throw new ArgumentException("count is less than or equal to 0.", "count");

            unsafe
            {
                uint* bids = stackalloc uint[count];
                Al.SourceUnqueueBuffers(Id, count, bids);

                Buffer[] buffers = new Buffer[count];
                for (int i = 0; i < count; ++i)
                {
                    buffers[i] = new Buffer(bids[i]);
                }
                return buffers;
            }
        }

        public override int GetHashCode()
        {
            AlHelper.ThrowNullException(Id);
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            AlHelper.ThrowNullException(Id);
            if (obj is Source)
            {
                return Equals((Source)obj);
            }
            return false;
        }

        public bool Equals(Source other)
        {
            AlHelper.ThrowNullException(Id);
            return Id == other.Id;
        }

        public static bool operator ==(Source left, Source right)
        {
            return left.Id == right.Id;
        }

        public static bool operator !=(Source left, Source right)
        {
            return left.Id != right.Id;
        }

        public override string ToString()
        {
            AlHelper.ThrowNullException(Id);
            return string.Format("Source: {0}", Id.ToString());
        }
    }
}
