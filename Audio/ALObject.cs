using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Audio
{
    public abstract class ALObject
    {
        internal int Id { get; private set; }

        public ALObject(int id)
        {
            Id = id;
            ObjectTable.Add(this);
        }

        internal bool IsBuffer { get { return OpenTK.Audio.OpenAL.AL.IsBuffer(Id); } }
        internal bool IsSource { get { return OpenTK.Audio.OpenAL.AL.IsSource(Id); } }

        public virtual void Delete()
        {
            ObjectTable.Remove(this);
        }
    }
}
