using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;

namespace Ibasa.OpenAL
{
    public enum SourceType
    {
        Static = AlSourceType.Static,
        Streaming = AlSourceType.Streaming,
        Undetermined = AlSourceType.Undetermined,
    }

    public enum SourceState
    {
        Initial = AlSourceState.Initial,
        Paused = AlSourceState.Paused,
        Playing = AlSourceState.Playing,
        Stopped = AlSourceState.Stopped,
    }

    public struct Source
    {
        internal uint Id { get; private set; }

        public static readonly Source Null = new Source(0);

        internal Source(uint sid)
            : this()
        {
            Id = sid;
            Contract.Assert(Id == 0 || Al.IsSource(Id));
        }

        public static Source Gen()
        {
            return new Source(Al.GenSource());
        }

        public void Delete()
        {
            Al.DeleteSource(Id);
        }

        public SourceType Type
        {
            get
            {
                int type;
                Al.GetSource(Id, AlGetSourcei.SourceType, out type);
                return (SourceType)type;
            }
        }

        public SourceState State
        {
            get
            {
                int state;
                Al.GetSource(Id, AlGetSourcei.SourceState, out state);
                return (SourceState)state;
            }
        }

        public int ByteOffset
        {
            get
            {
                int value;
                Al.GetSource(Id, AlGetSourcei.ByteOffset, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcei.ByteOffset, value);
            }
        }

        public int SampleOffset
        {
            get
            {
                int value;
                Al.GetSource(Id, AlGetSourcei.SampleOffset, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcei.SampleOffset, value);
            }
        }

        public int BuffersProcessed
        {
            get
            {
                int value;
                Al.GetSource(Id, AlGetSourcei.BuffersProcessed, out value);
                return value;
            }
        }

        public int BuffersQueued
        {
            get
            {
                int value;
                Al.GetSource(Id, AlGetSourcei.BuffersQueued, out value);
                return value;
            }
        }

        public Buffer Buffer
        {
            get
            {
                int id;
                Al.GetSource(Id, AlGetSourcei.Buffer, out id);
                return new Buffer((uint)id);
            }
            set
            {
                Al.Source(Id, AlSourcei.Buffer, (int)value.Id);
            }
        }

        public bool Looping
        {
            get
            {
                bool value;
                Al.GetSource(Id, AlSourceb.Looping, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourceb.Looping, value);
            }
        }

        public bool SourceRelative
        {
            get
            {
                bool value;
                Al.GetSource(Id, AlSourceb.SourceRelative, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourceb.SourceRelative, value);
            }
        }

        public Vector3f Direction
        {
            get
            {
                float x, y, z;
                Al.GetSource(Id, AlSource3f.Direction, out x, out y, out z);
                return new Vector3f(x, y, z);
            }
            set
            {
                Al.Source(Id, AlSource3f.Direction, value.X, value.Y, value.Z);
            }
        }

        public Point3f Position
        {
            get
            {
                float x, y, z;
                Al.GetSource(Id, AlSource3f.Position, out x, out y, out z);
                return new Point3f(x, y, z);
            }
            set
            {
                Al.Source(Id, AlSource3f.Position, value.X, value.Y, value.Z);
            }
        }

        public Vector3f Velocity
        {
            get
            {
                float x, y, z;
                Al.GetSource(Id, AlSource3f.Velocity, out x, out y, out z);
                return new Vector3f(x, y, z);
            }
            set
            {
                Al.Source(Id, AlSource3f.Velocity, value.X, value.Y, value.Z);
            }
        }

        public float ConeInnerAngle
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.ConeInnerAngle, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.ConeInnerAngle, value);
            }
        }

        public float ConeOuterAngle
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.ConeOuterAngle, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.ConeOuterAngle, value);
            }
        }

        public float ConeOuterGain
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.ConeOuterGain, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.ConeOuterGain, value);
            }
        }

        public float EfxAirAbsorptionFactor
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.EfxAirAbsorptionFactor, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.EfxAirAbsorptionFactor, value);
            }
        }

        public float EfxConeOuterGainHighFrequency
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.EfxConeOuterGainHighFrequency, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.EfxConeOuterGainHighFrequency, value);
            }
        }

        public float EfxRoomRolloffFactor
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.EfxRoomRolloffFactor, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.EfxRoomRolloffFactor, value);
            }
        }

        public float Gain
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.Gain, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.Gain, value);
            }
        }

        public float MaxDistance
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.MaxDistance, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.MaxDistance, value);
            }
        }

        public float MaxGain
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.MaxGain, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.MaxGain, value);
            }
        }

        public float MinGain
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.MinGain, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.MinGain, value);
            }
        }

        public float Pitch
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.Pitch, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.Pitch, value);
            }
        }

        public float ReferenceDistance
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.ReferenceDistance, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.ReferenceDistance, value);
            }
        }

        public float RolloffFactor
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.RolloffFactor, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.RolloffFactor, value);
            }
        }

        public float SecOffset
        {
            get
            {
                float value;
                Al.GetSource(Id, AlSourcef.SecOffset, out value);
                return value;
            }
            set
            {
                Al.Source(Id, AlSourcef.SecOffset, value);
            }
        }

        public void Pause()
        {
            Al.SourcePause(Id);
        }

        public void Play()
        {
            Al.SourcePlay(Id);
        }

        public void Rewind()
        {
            Al.SourceRewind(Id);
        }

        public void Stop()
        {
            Al.SourceStop(Id);
        }

        public void Queue(Buffer buffer)
        {
            Al.SourceQueueBuffer(Id, buffer.Id);
        }

        public void Queue(Buffer[] buffers)
        {
            uint[] bids = new uint[buffers.Length];
            for (int i = 0; i < bids.Length; ++i)
            {
                bids[i] = buffers[i].Id;
            }
            Al.SourceQueueBuffers(Id, buffers.Length, bids);
        }

        public Buffer Unqueue()
        {
            var bid = Al.SourceUnqueueBuffer(Id);
            return new Buffer(bid);
        }

        public Buffer[] Unqueue(int count)
        {
            uint[] bids = new uint[count];
            Al.SourceUnqueueBuffers(Id, count, bids);
            var buffers = new Buffer[count];
            for (int i = 0; i < count; ++i)
            {
                buffers[i] = new Buffer(bids[i]);
            }
            return buffers;
        }
    }
}
