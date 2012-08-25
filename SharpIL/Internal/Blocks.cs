using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ibasa.Numerics;
using System.Collections.ObjectModel;

namespace Ibasa.SharpIL.Internal
{
	class ColordBlock
	{
        public ColordBlock()
        {
            Indices = new int[16];
        }

        public int Colord0 { get; set; }
        public int Colord1 { get; set; }
		public int[] Indices {get; private set;}
	}

    class ChannelBlock
	{
        public ChannelBlock()
        {
            Indices = new int[16];
        }
		
		public double Colord0 {get; set;}
		public double Colord1 {get; set;}
		public int[] Indices {get; private set;}
	}
}