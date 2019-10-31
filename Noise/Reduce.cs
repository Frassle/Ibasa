using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    /// <summary>
    /// Reduces a collection of source modules with the given function.
    /// </summary>
    public sealed class Reduce<TSource> : Module<TSource>
    {
        #region Source
        private IEnumerable<Module<TSource>> property_Source;
        /// <summary>
        /// Source modules.
        /// </summary>
        public IEnumerable<Module<TSource>> Source
        {
            get { return property_Source; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                if(value.Any(module => module == null))
                    throw new global::System.ArgumentNullException("value", "The collection contanins a null value.");

                property_Source = value;
            }
        }
        #endregion

        #region Function
        private Func<TSource, TSource, TSource> property_Function;
        /// <summary>
        /// An accumulator function to be invoked on each element.
        /// </summary>
        public Func<TSource, TSource, TSource> Function
        {
            get { return property_Function; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Function = value;
            }
        }
        #endregion

        public Reduce(IEnumerable<Module<TSource>> source, Func<TSource, TSource, TSource> function)
        {
            Source = source;
            Function = function;
        }

        public override TSource Evaluate(double x)
        {
            var enumerator = Source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var module = enumerator.Current;
                var result = module.Evaluate(x);

                while (enumerator.MoveNext())
                {
                    module = enumerator.Current;
                    result = Function(result, module.Evaluate(x));
                }

                return result;
            }

            throw new InvalidOperationException("source contains no elements.");
        }

        public override TSource Evaluate(double x, double y)
        {
            var enumerator = Source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var module = enumerator.Current;
                var result = module.Evaluate(x, y);

                while (enumerator.MoveNext())
                {
                    module = enumerator.Current;
                    result = Function(result, module.Evaluate(x, y));
                }

                return result;
            }

            throw new InvalidOperationException("source contains no elements.");
        }

        public override TSource Evaluate(double x, double y, double z)
        {
            var enumerator = Source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var module = enumerator.Current;
                var result = module.Evaluate(x, y, z);

                while (enumerator.MoveNext())
                {
                    module = enumerator.Current;
                    result = Function(result, module.Evaluate(x, y, z));
                }

                return result;
            }

            throw new InvalidOperationException("source contains no elements.");
        }

        public override TSource Evaluate(double x, double y, double z, double w)
        {
            var enumerator = Source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var module = enumerator.Current;
                var result = module.Evaluate(x, y, z, w);

                while (enumerator.MoveNext())
                {
                    module = enumerator.Current;
                    result = Function(result, module.Evaluate(x, y, z, w));
                }

                return result;
            }

            throw new InvalidOperationException("source contains no elements.");
        }

        public override TSource Evaluate(double x, double y, double z, double w, double v, double u)
        {
            var enumerator = Source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var module = enumerator.Current;
                var result = module.Evaluate(x, y, z, w, v, u);

                while (enumerator.MoveNext())
                {
                    module = enumerator.Current;
                    result = Function(result, module.Evaluate(x, y, z, w, v, u));
                }

                return result;
            }

            throw new InvalidOperationException("source contains no elements.");
        }
    }

    /// <summary>
    /// Reduces a collection of source modules with the given function.
    /// </summary>
    public sealed class Reduce<TSource, TAccumulate> : Module<TAccumulate>
    {
        #region Source
        private IEnumerable<Module<TSource>> property_Source;
        /// <summary>
        /// Source modules.
        /// </summary>
        public IEnumerable<Module<TSource>> Source
        {
            get { return property_Source; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                if (value.Any(module => module == null))
                    throw new global::System.ArgumentNullException("value", "The collection contanins a null value.");

                property_Source = value;
            }
        }
        #endregion

        #region Seed
        private Module<TAccumulate> property_Seed;
        /// <summary>
        /// Seed module.
        /// </summary>
        public Module<TAccumulate> Seed
        {
            get { return property_Seed; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");

                property_Seed = value;
            }
        }
        #endregion

        #region Function
        private Func<TAccumulate, TSource, TAccumulate> property_Function;
        /// <summary>
        /// An accumulator function to be invoked on each element.
        /// </summary>
        public Func<TAccumulate, TSource, TAccumulate> Function
        {
            get { return property_Function; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Function = value;
            }
        }
        #endregion

        public Reduce(IEnumerable<Module<TSource>> source, Module<TAccumulate> seed, Func<TAccumulate, TSource, TAccumulate> function)
        {
            Source = source;
            Seed = seed;
            Function = function;
        }

        public override TAccumulate Evaluate(double x)
        {
            var result = Seed.Evaluate(x);

            foreach(var module in Source)
            {
                result = Function(result, module.Evaluate(x));
            }

            return result;
        }

        public override TAccumulate Evaluate(double x, double y)
        {
            var result = Seed.Evaluate(x, y);

            foreach (var module in Source)
            {
                result = Function(result, module.Evaluate(x, y));
            }

            return result;
        }

        public override TAccumulate Evaluate(double x, double y, double z)
        {
            var result = Seed.Evaluate(x, y, z);

            foreach (var module in Source)
            {
                result = Function(result, module.Evaluate(x, y, z));
            }

            return result;
        }

        public override TAccumulate Evaluate(double x, double y, double z, double w)
        {
            var result = Seed.Evaluate(x, y, z, w);

            foreach (var module in Source)
            {
                result = Function(result, module.Evaluate(x, y, z, w));
            }

            return result;
        }

        public override TAccumulate Evaluate(double x, double y, double z, double w, double v, double u)
        {
            var result = Seed.Evaluate(x, y, z, w, v, u);

            foreach (var module in Source)
            {
                result = Function(result, module.Evaluate(x, y, z, w, v, u));
            }

            return result;
        }
    }
}
