using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public sealed class Source : IDisposable
    {
        internal readonly int Sid;

        private Source(int sid)
        {
            Sid = sid;
        }

        public Source()
        {
            Sid = OpenTK.Audio.OpenAL.AL.GenSource();
        }

        ~Source()
        {
            Dispose();
        }

        public void Dispose()
        {
            OpenTK.Audio.OpenAL.AL.DeleteSource(Sid);
            GC.SuppressFinalize(this);
        }

        Source[] Create(int count)
        {
            var names = OpenTK.Audio.OpenAL.AL.GenSources(count);
            Source[] sources = new Source[count];
            for (int i = 0; i < count; ++i)
            {
                sources[i] = new Source(names[i]);
            }
            return sources;
        }

        public void Queue(Buffer buffer)
        {
            OpenTK.Audio.OpenAL.AL.SourceQueueBuffer(Sid, buffer.Bid);
        }

        public void Queue(Buffer[] buffers)
        {
            int[] bids = new int[buffers.Length];
            for (int i = 0; i < bids.Length; ++i)
            {
                bids[i] = buffers[i].Bid;
            }
            OpenTK.Audio.OpenAL.AL.SourceQueueBuffers(Sid, buffers.Length, bids);
        }
    }
}
