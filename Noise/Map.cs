using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Noise
{
    /// <summary>
    /// Maps two source modules together with the given function.
    /// </summary>
    public sealed class Map<T, TResult> : Module<TResult>
    {
        #region Source
        private Module<T> property_Source;
        /// <summary>
        /// Source module.
        /// </summary>
        public Module<T> Source
        {
            get { return property_Source; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source = value;
            }
        }
        #endregion

        #region Function
        private Func<T, TResult> property_Function;
        /// <summary>
        /// Function to apply to source.
        /// </summary>
        public Func<T, TResult> Function
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

        public Map(Module<T> source, Func<T, TResult> function)
        {
            Source = source;
            Function = function;
        }

        public override TResult Evaluate(double x)
        {
            return Function(Source.Evaluate(x));
        }

        public override TResult Evaluate(double x, double y)
        {
            return Function(Source.Evaluate(x, y));
        }

        public override TResult Evaluate(double x, double y, double z)
        {
            return Function(Source.Evaluate(x, y, z));
        }

        public override TResult Evaluate(double x, double y, double z, double w)
        {
            return Function(Source.Evaluate(x, y, z, w));
        }

        public override TResult Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Function(Source.Evaluate(x, y, z, w, v, u));
        }
    }

    /// <summary>
    /// Maps two source modules together with the given function.
    /// </summary>
    public sealed class Map<T1, T2, TResult> : Module<TResult>
    {
        #region Source1
        private Module<T1> property_Source1;
        /// <summary>
        /// Source module one.
        /// </summary>
        public Module<T1> Source1
        {
            get { return property_Source1; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source1 = value;
            }
        }
        #endregion

        #region Source2
        private Module<T2> property_Source2;
        /// <summary>
        /// Source module two.
        /// </summary>
        public Module<T2> Source2
        {
            get { return property_Source2; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source2 = value;
            }
        }
        #endregion
        
        #region Function
        private Func<T1, T2, TResult> property_Function;
        /// <summary>
        /// Function to apply to sources.
        /// </summary>
        public Func<T1, T2, TResult> Function
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

        public Map(Module<T1> source1, Module<T2> source2, Func<T1, T2, TResult> function)
        {
            Source1 = source1;
            Source2 = source2;
            Function = function;
        }

        public override TResult Evaluate(double x)
        {
            return Function(Source1.Evaluate(x), Source2.Evaluate(x));
        }

        public override TResult Evaluate(double x, double y)
        {
            return Function(Source1.Evaluate(x, y), Source2.Evaluate(x, y));
        }

        public override TResult Evaluate(double x, double y, double z)
        {
            return Function(Source1.Evaluate(x, y, z), Source2.Evaluate(x, y, z));
        }

        public override TResult Evaluate(double x, double y, double z, double w)
        {
            return Function(Source1.Evaluate(x, y, z, w), Source2.Evaluate(x, y, z, w));
        }

        public override TResult Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Function(Source1.Evaluate(x, y, z, w, v, u), Source2.Evaluate(x, y, z, w, v, u));
        }
    }

    /// <summary>
    /// Maps three source modules together with the given function.
    /// </summary>
    public sealed class Map<T1, T2, T3, TResult> : Module<TResult>
    {
        #region Source1
        private Module<T1> property_Source1;
        /// <summary>
        /// Source module one.
        /// </summary>
        public Module<T1> Source1
        {
            get { return property_Source1; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source1 = value;
            }
        }
        #endregion

        #region Source2
        private Module<T2> property_Source2;
        /// <summary>
        /// Source module two.
        /// </summary>
        public Module<T2> Source2
        {
            get { return property_Source2; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source2 = value;
            }
        }
        #endregion

        #region Source3
        private Module<T3> property_Source3;
        /// <summary>
        /// Source module three.
        /// </summary>
        public Module<T3> Source3
        {
            get { return property_Source3; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source3 = value;
            }
        }
        #endregion

        #region Function
        private Func<T1, T2, T3, TResult> property_Function;
        /// <summary>
        /// Function to apply to sources.
        /// </summary>
        public Func<T1, T2, T3, TResult> Function
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

        public Map(Module<T1> source1, Module<T2> source2, Module<T3> source3, Func<T1, T2, T3, TResult> function)
        {
            Source1 = source1;
            Source2 = source2;
            Source3 = source3;
            Function = function;
        }

        public override TResult Evaluate(double x)
        {
            return Function(
                Source1.Evaluate(x),
                Source2.Evaluate(x),
                Source3.Evaluate(x));
        }

        public override TResult Evaluate(double x, double y)
        {
            return Function(
                Source1.Evaluate(x, y),
                Source2.Evaluate(x, y),
                Source3.Evaluate(x, y));
        }

        public override TResult Evaluate(double x, double y, double z)
        {
            return Function(
                Source1.Evaluate(x, y, z),
                Source2.Evaluate(x, y, z),
                Source3.Evaluate(x, y, z));
        }

        public override TResult Evaluate(double x, double y, double z, double w)
        {
            return Function(
                Source1.Evaluate(x, y, z, w), 
                Source2.Evaluate(x, y, z, w), 
                Source3.Evaluate(x, y, z, w));
        }

        public override TResult Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Function(
                Source1.Evaluate(x, y, z, w, v, u),
                Source2.Evaluate(x, y, z, w, v, u),
                Source3.Evaluate(x, y, z, w, v, u));
        }
    }

    /// <summary>
    /// Maps four source modules together with the given function.
    /// </summary>
    public sealed class Map<T1, T2, T3, T4, TResult> : Module<TResult>
    {
        #region Source1
        private Module<T1> property_Source1;
        /// <summary>
        /// Source module one.
        /// </summary>
        public Module<T1> Source1
        {
            get { return property_Source1; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source1 = value;
            }
        }
        #endregion

        #region Source2
        private Module<T2> property_Source2;
        /// <summary>
        /// Source module two.
        /// </summary>
        public Module<T2> Source2
        {
            get { return property_Source2; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source2 = value;
            }
        }
        #endregion

        #region Source3
        private Module<T3> property_Source3;
        /// <summary>
        /// Source module three.
        /// </summary>
        public Module<T3> Source3
        {
            get { return property_Source3; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source3 = value;
            }
        }
        #endregion

        #region Source4
        private Module<T4> property_Source4;
        /// <summary>
        /// Source module four.
        /// </summary>
        public Module<T4> Source4
        {
            get { return property_Source4; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source4 = value;
            }
        }
        #endregion

        #region Function
        private Func<T1, T2, T3, T4, TResult> property_Function;
        /// <summary>
        /// Function to apply to sources.
        /// </summary>
        public Func<T1, T2, T3, T4, TResult> Function
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

        public Map(Module<T1> source1, Module<T2> source2, Module<T3> source3, Module<T4> source4, Func<T1, T2, T3, T4, TResult> function)
        {
            Source1 = source1;
            Source2 = source2;
            Source3 = source3;
            Source4 = source4;
            Function = function;
        }

        public override TResult Evaluate(double x)
        {
            return Function(
                Source1.Evaluate(x),
                Source2.Evaluate(x),
                Source3.Evaluate(x),
                Source4.Evaluate(x));
        }

        public override TResult Evaluate(double x, double y)
        {
            return Function(
                Source1.Evaluate(x, y),
                Source2.Evaluate(x, y),
                Source3.Evaluate(x, y),
                Source4.Evaluate(x, y));
        }

        public override TResult Evaluate(double x, double y, double z)
        {
            return Function(
                Source1.Evaluate(x, y, z),
                Source2.Evaluate(x, y, z),
                Source3.Evaluate(x, y, z),
                Source4.Evaluate(x, y, z));
        }

        public override TResult Evaluate(double x, double y, double z, double w)
        {
            return Function(
                Source1.Evaluate(x, y, z, w),
                Source2.Evaluate(x, y, z, w),
                Source3.Evaluate(x, y, z, w),
                Source4.Evaluate(x, y, z, w));
        }

        public override TResult Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Function(
                Source1.Evaluate(x, y, z, w, v, u),
                Source2.Evaluate(x, y, z, w, v, u),
                Source3.Evaluate(x, y, z, w, v, u),
                Source4.Evaluate(x, y, z, w, v, u));
        }
    }
    
    /// <summary>
    /// Maps sixteen source modules together with the given function.
    /// </summary>
    public sealed class Map<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> : Module<TResult>
    {
        #region Source1
        private Module<T1> property_Source1;
        /// <summary>
        /// Source module one.
        /// </summary>
        public Module<T1> Source1
        {
            get { return property_Source1; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source1 = value;
            }
        }
        #endregion

        #region Source2
        private Module<T2> property_Source2;
        /// <summary>
        /// Source module two.
        /// </summary>
        public Module<T2> Source2
        {
            get { return property_Source2; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source2 = value;
            }
        }
        #endregion

        #region Source3
        private Module<T3> property_Source3;
        /// <summary>
        /// Source module three.
        /// </summary>
        public Module<T3> Source3
        {
            get { return property_Source3; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source3 = value;
            }
        }
        #endregion

        #region Source4
        private Module<T4> property_Source4;
        /// <summary>
        /// Source module four.
        /// </summary>
        public Module<T4> Source4
        {
            get { return property_Source4; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source4 = value;
            }
        }
        #endregion

        #region Source5
        private Module<T5> property_Source5;
        /// <summary>
        /// Source module five.
        /// </summary>
        public Module<T5> Source5
        {
            get { return property_Source5; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source5 = value;
            }
        }
        #endregion

        #region Source6
        private Module<T6> property_Source6;
        /// <summary>
        /// Source module six.
        /// </summary>
        public Module<T6> Source6
        {
            get { return property_Source6; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source6 = value;
            }
        }
        #endregion

        #region Source7
        private Module<T7> property_Source7;
        /// <summary>
        /// Source module seven.
        /// </summary>
        public Module<T7> Source7
        {
            get { return property_Source7; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source7 = value;
            }
        }
        #endregion

        #region Source8
        private Module<T8> property_Source8;
        /// <summary>
        /// Source module eight.
        /// </summary>
        public Module<T8> Source8
        {
            get { return property_Source8; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source8 = value;
            }
        }
        #endregion

        #region Source9
        private Module<T9> property_Source9;
        /// <summary>
        /// Source module nine.
        /// </summary>
        public Module<T9> Source9
        {
            get { return property_Source9; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source9 = value;
            }
        }
        #endregion

        #region Source10
        private Module<T10> property_Source10;
        /// <summary>
        /// Source module ten.
        /// </summary>
        public Module<T10> Source10
        {
            get { return property_Source10; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source10 = value;
            }
        }
        #endregion

        #region Source11
        private Module<T11> property_Source11;
        /// <summary>
        /// Source module eleven.
        /// </summary>
        public Module<T11> Source11
        {
            get { return property_Source11; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source11 = value;
            }
        }
        #endregion

        #region Source12
        private Module<T12> property_Source12;
        /// <summary>
        /// Source module twelve.
        /// </summary>
        public Module<T12> Source12
        {
            get { return property_Source12; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source12 = value;
            }
        }
        #endregion

        #region Source13
        private Module<T13> property_Source13;
        /// <summary>
        /// Source module thirteen.
        /// </summary>
        public Module<T13> Source13
        {
            get { return property_Source13; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source13 = value;
            }
        }
        #endregion

        #region Source14
        private Module<T14> property_Source14;
        /// <summary>
        /// Source module fourteen.
        /// </summary>
        public Module<T14> Source14
        {
            get { return property_Source14; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source14 = value;
            }
        }
        #endregion

        #region Source15
        private Module<T15> property_Source15;
        /// <summary>
        /// Source module fifeteen.
        /// </summary>
        public Module<T15> Source15
        {
            get { return property_Source15; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source15 = value;
            }
        }
        #endregion

        #region Source16
        private Module<T16> property_Source16;
        /// <summary>
        /// Source module sixteen.
        /// </summary>
        public Module<T16> Source16
        {
            get { return property_Source16; }
            set
            {
                if (value == null)
                    throw new global::System.ArgumentNullException("value", "The value for this property is null.");
                property_Source16 = value;
            }
        }
        #endregion

        #region Function
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> property_Function;
        /// <summary>
        /// Function to apply to sources.
        /// </summary>
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Function
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

        public Map(
            Module<T1> source1, Module<T2> source2, Module<T3> source3, Module<T4> source4,
            Module<T5> source5, Module<T6> source6, Module<T7> source7, Module<T8> source8,
            Module<T9> source9, Module<T10> source10, Module<T11> source11, Module<T12> source12,
            Module<T13> source13, Module<T14> source14, Module<T15> source15, Module<T16> source16,             
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function)
        {
            Source1 = source1;
            Source2 = source2;
            Source3 = source3;
            Source4 = source4;
            Source5 = source5;
            Source6 = source6;
            Source7 = source7;
            Source8 = source8;
            Source9 = source9;
            Source10 = source10;
            Source11 = source11;
            Source12 = source12;
            Source13 = source13;
            Source14 = source14;
            Source15 = source15;
            Source16 = source16;
            Function = function;
        }

        public override TResult Evaluate(double x)
        {
            return Function(
                Source1.Evaluate(x),
                Source2.Evaluate(x),
                Source3.Evaluate(x),
                Source4.Evaluate(x),
                Source5.Evaluate(x),
                Source6.Evaluate(x),
                Source7.Evaluate(x),
                Source8.Evaluate(x),
                Source9.Evaluate(x),
                Source10.Evaluate(x),
                Source11.Evaluate(x),
                Source12.Evaluate(x),
                Source13.Evaluate(x),
                Source14.Evaluate(x),
                Source15.Evaluate(x),
                Source16.Evaluate(x));
        }

        public override TResult Evaluate(double x, double y)
        {
            return Function(
                Source1.Evaluate(x, y),
                Source2.Evaluate(x, y),
                Source3.Evaluate(x, y),
                Source4.Evaluate(x, y),
                Source5.Evaluate(x, y),
                Source6.Evaluate(x, y),
                Source7.Evaluate(x, y),
                Source8.Evaluate(x, y),
                Source9.Evaluate(x, y),
                Source10.Evaluate(x, y),
                Source11.Evaluate(x, y),
                Source12.Evaluate(x, y),
                Source13.Evaluate(x, y),
                Source14.Evaluate(x, y),
                Source15.Evaluate(x, y),
                Source16.Evaluate(x, y));
        }

        public override TResult Evaluate(double x, double y, double z)
        {
            return Function(
                Source1.Evaluate(x, y, z),
                Source2.Evaluate(x, y, z),
                Source3.Evaluate(x, y, z),
                Source4.Evaluate(x, y, z),
                Source5.Evaluate(x, y, z),
                Source6.Evaluate(x, y, z),
                Source7.Evaluate(x, y, z),
                Source8.Evaluate(x, y, z),
                Source9.Evaluate(x, y, z),
                Source10.Evaluate(x, y, z),
                Source11.Evaluate(x, y, z),
                Source12.Evaluate(x, y, z),
                Source13.Evaluate(x, y, z),
                Source14.Evaluate(x, y, z),
                Source15.Evaluate(x, y, z),
                Source16.Evaluate(x, y, z));
        }

        public override TResult Evaluate(double x, double y, double z, double w)
        {
            return Function(
                Source1.Evaluate(x, y, z, w),
                Source2.Evaluate(x, y, z, w),
                Source3.Evaluate(x, y, z, w),
                Source4.Evaluate(x, y, z, w),
                Source5.Evaluate(x, y, z, w),
                Source6.Evaluate(x, y, z, w),
                Source7.Evaluate(x, y, z, w),
                Source8.Evaluate(x, y, z, w),
                Source9.Evaluate(x, y, z, w),
                Source10.Evaluate(x, y, z, w),
                Source11.Evaluate(x, y, z, w),
                Source12.Evaluate(x, y, z, w),
                Source13.Evaluate(x, y, z, w),
                Source14.Evaluate(x, y, z, w),
                Source15.Evaluate(x, y, z, w),
                Source16.Evaluate(x, y, z, w));
        }

        public override TResult Evaluate(double x, double y, double z, double w, double v, double u)
        {
            return Function(
                Source1.Evaluate(x, y, z, w, v, u),
                Source2.Evaluate(x, y, z, w, v, u),
                Source3.Evaluate(x, y, z, w, v, u),
                Source4.Evaluate(x, y, z, w, v, u),
                Source5.Evaluate(x, y, z, w, v, u),
                Source6.Evaluate(x, y, z, w, v, u),
                Source7.Evaluate(x, y, z, w, v, u),
                Source8.Evaluate(x, y, z, w, v, u),
                Source9.Evaluate(x, y, z, w, v, u),
                Source10.Evaluate(x, y, z, w, v, u),
                Source11.Evaluate(x, y, z, w, v, u),
                Source12.Evaluate(x, y, z, w, v, u),
                Source13.Evaluate(x, y, z, w, v, u),
                Source14.Evaluate(x, y, z, w, v, u),
                Source15.Evaluate(x, y, z, w, v, u),
                Source16.Evaluate(x, y, z, w, v, u));
        }
    }
}
