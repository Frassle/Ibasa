using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numerics_Generator
{
    abstract class Generator
    {
        private StringBuilder Builder;
        private List<string> Indents;
        bool DoIndent;

        protected Generator()
        {
            Builder = new StringBuilder();
            Indents = new List<string>();
            DoIndent = true;
        }

        protected void Write(string text)
        {
            if (DoIndent)
            {
                foreach (var str in Indents)
                {
                    Builder.Append(str);
                }
            }
            DoIndent = false;
            Builder.Append(text);
        }

        protected void Write(string format, params object[] args)
        {
            if (DoIndent)
            {
                foreach (var str in Indents)
                {
                    Builder.Append(str);
                }
            }
            DoIndent = false;
            Builder.AppendFormat(format, args);
        }

        protected void WriteLine(string text)
        {
            if (DoIndent)
            {
                foreach (var str in Indents)
                {
                    Builder.Append(str);
                }
            }
            DoIndent = true;
            Builder.AppendLine(text);
        }

        protected void WriteLine(string text, params object[] args)
        {
            if (DoIndent)
            {
                foreach (var str in Indents)
                {
                    Builder.Append(str);
                }
            }
            DoIndent = true;
            Builder.AppendFormat(text, args);
            Builder.AppendLine();
        }

        protected void Indent(string pre = "", string indent = "\t", string post = "")
        {
            if (pre != string.Empty)
                WriteLine(pre);
            Indents.Add(indent);
            if (post != string.Empty)
                WriteLine(post);
        }

        protected void Dedent(string post = "", string pre = "")
        {
            if (pre != string.Empty)
                WriteLine(pre);
            Indents.RemoveAt(Indents.Count - 1);
            if (post != string.Empty)
                WriteLine(post);
        }

        public string Text { get { return Builder.ToString(); } }
    }
}
