using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    public sealed class Constant<T> : Module<T>
    {
        T Value { get; set; }

        public Constant(T value)
        {
            Value = value;
        }

        public override T Evaluate(double x)
        {
            return Value;
        }

        public override T Evaluate(double x, double y)
        {
            return Value;
        }

        public override T Evaluate(double x, double y, double z)
        {
            return Value;
        }

        public override T Evaluate(double x, double y, double z, double w)
        {
            return Value;
        }

        public override T Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Value;
        }
    }
}
