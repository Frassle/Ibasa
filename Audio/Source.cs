using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics.Geometry;
using System.Diagnostics.Contracts;

namespace Ibasa.Audio
{
    public enum SourceType
    {
        Static = OpenTK.Audio.OpenAL.ALSourceType.Static,
        Streaming = OpenTK.Audio.OpenAL.ALSourceType.Streaming,
        Undetermined = OpenTK.Audio.OpenAL.ALSourceType.Undetermined,
    }

    public enum SourceState
    {
        Initial = OpenTK.Audio.OpenAL.ALSourceState.Initial,
        Paused = OpenTK.Audio.OpenAL.ALSourceState.Paused,
        Playing = OpenTK.Audio.OpenAL.ALSourceState.Playing,
        Stopped = OpenTK.Audio.OpenAL.ALSourceState.Stopped,
    }

    public struct Source
    {
        internal uint Id { get; private set; }

        public static readonly Source Null = new Source(0);

        internal Source(uint sid)
            : this()
        {
            Id = sid;
            Contract.Assert(Id == 0 || OpenTK.Audio.OpenAL.AL.IsSource(Id));
        }

        public static Source Gen()
        {
            return new Source(OpenTK.Audio.OpenAL.AL.GenSource());
        }

        public void Delete()
        {
            OpenTK.Audio.OpenAL.AL.DeleteSource(Id);
            Context.ThrowIfError();
        }

        public SourceType Type
        {
            get
            {
                int type;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALGetSourcei.SourceType, out type);
                return (SourceType)type;
            }
        }

        public SourceState State
        {
            get
            {
                int state;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALGetSourcei.SourceState, out state);
                return (SourceState)state;
            }
        }

        public int ByteOffset
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALGetSourcei.ByteOffset, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcei.ByteOffset, value);
            }
        }

        public int SampleOffset
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALGetSourcei.SampleOffset, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcei.SampleOffset, value);
            }
        }

        public int BuffersProcessed
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALGetSourcei.BuffersProcessed, out value);
                return value;
            }
        }

        public int BuffersQueued
        {
            get
            {
                int value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALGetSourcei.BuffersQueued, out value);
                return value;
            }
        }

        public Buffer Buffer
        {
            get
            {
                int id;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALGetSourcei.Buffer, out id);
                return new Buffer((uint)id);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcei.Buffer, (int)value.Id);
                Context.ThrowIfError();
            }
        }

        public bool Looping
        {
            get
            {
                bool value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourceb.Looping, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourceb.Looping, value);
            }
        }

        public bool SourceRelative
        {
            get
            {
                bool value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourceb.SourceRelative, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourceb.SourceRelative, value);
            }
        }

        public Vector3f Direction
        {
            get
            {
                float x, y, z;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSource3f.Direction, out x, out y, out z);
                return new Vector3f(x, y, z);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSource3f.Direction, value.X, value.Y, value.Z);
            }
        }

        public Point3f Position
        {
            get
            {
                float x, y, z;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSource3f.Position, out x, out y, out z);
                return new Point3f(x, y, z);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSource3f.Position, value.X, value.Y, value.Z);
            }
        }

        public Vector3f Velocity
        {
            get
            {
                float x, y, z;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSource3f.Velocity, out x, out y, out z);
                return new Vector3f(x, y, z);
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSource3f.Velocity, value.X, value.Y, value.Z);
            }
        }

        public float ConeInnerAngle
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.ConeInnerAngle, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.ConeInnerAngle, value);
            }
        }

        public float ConeOuterAngle
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.ConeOuterAngle, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.ConeOuterAngle, value);
            }
        }

        public float ConeOuterGain
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.ConeOuterGain, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.ConeOuterGain, value);
            }
        }

        public float EfxAirAbsorptionFactor
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.EfxAirAbsorptionFactor, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.EfxAirAbsorptionFactor, value);
            }
        }

        public float EfxConeOuterGainHighFrequency
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.EfxConeOuterGainHighFrequency, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.EfxConeOuterGainHighFrequency, value);
            }
        }

        public float EfxRoomRolloffFactor
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.EfxRoomRolloffFactor, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.EfxRoomRolloffFactor, value);
            }
        }

        public float Gain
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.Gain, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.Gain, value);
            }
        }

        public float MaxDistance
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.MaxDistance, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.MaxDistance, value);
            }
        }

        public float MaxGain
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.MaxGain, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.MaxGain, value);
            }
        }

        public float MinGain
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.MinGain, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.MinGain, value);
            }
        }

        public float Pitch
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.Pitch, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.Pitch, value);
            }
        }

        public float ReferenceDistance
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.ReferenceDistance, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.ReferenceDistance, value);
            }
        }

        public float RolloffFactor
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.RolloffFactor, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.RolloffFactor, value);
            }
        }

        public float SecOffset
        {
            get
            {
                float value;
                OpenTK.Audio.OpenAL.AL.GetSource(Id, OpenTK.Audio.OpenAL.ALSourcef.SecOffset, out value);
                return value;
            }
            set
            {
                OpenTK.Audio.OpenAL.AL.Source(Id, OpenTK.Audio.OpenAL.ALSourcef.SecOffset, value);
            }
        }

        public void Pause()
        {
            OpenTK.Audio.OpenAL.AL.SourcePause(Id);
        }

        public void Play()
        {
            OpenTK.Audio.OpenAL.AL.SourcePlay(Id);
            Context.ThrowIfError();
        }

        public void Rewind()
        {
            OpenTK.Audio.OpenAL.AL.SourceRewind(Id);
            Context.ThrowIfError();
        }

        public void Stop()
        {
            OpenTK.Audio.OpenAL.AL.SourceStop(Id);
            Context.ThrowIfError();
        }

        public void Queue(Buffer buffer)
        {
            OpenTK.Audio.OpenAL.AL.SourceQueueBuffer(Id, buffer.Id);
            Context.ThrowIfError();
        }

        public void Queue(Buffer[] buffers)
        {
            uint[] bids = new uint[buffers.Length];
            for (int i = 0; i < bids.Length; ++i)
            {
                bids[i] = buffers[i].Id;
            }
            OpenTK.Audio.OpenAL.AL.SourceQueueBuffers(Id, buffers.Length, bids);
            Context.ThrowIfError();
        }

        public Buffer Unqueue()
        {
            var bid = OpenTK.Audio.OpenAL.AL.SourceUnqueueBuffer(Id);
            Context.ThrowIfError();
            return new Buffer(bid);
        }

        public Buffer[] Unqueue(int count)
        {
            uint[] bids = new uint[count];
            OpenTK.Audio.OpenAL.AL.SourceUnqueueBuffers(Id, count, bids);
            Context.ThrowIfError();
            var buffers = new Buffer[count];
            for (int i = 0; i < count; ++i)
            {
                buffers[i] = new Buffer(bids[i]);
            }
            return buffers;
        }
    }
}
