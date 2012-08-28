using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    public sealed class Message
    {
        public Message(object sender, string method, params object[] args)
        {
            Sender = sender;
            Method = method;
            Args = new Collections.Immutable.ImmutableArray<object>(args);
        }

        public object Sender { get; private set; }
        public string Method { get; private set; }
        public Ibasa.Collections.Immutable.ImmutableArray<object> Args { get; private set; }
    }
}
