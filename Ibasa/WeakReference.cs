using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace Ibasa
{
    /// <summary>
    /// Represents a weak reference, which references an object while still allowing
    /// that object to be reclaimed by garbage collection.
    /// </summary>
    /// <typeparam name="T">The type of the object being referenced.</typeparam>
    public class WeakReference<T>
        where T : class
    {
        protected WeakReference Reference;

        /// <summary>
        /// Gets or sets the object (the target) referenced by the current WeakReference
        /// object.
        /// </summary>
        /// <returns>
        /// null if the object referenced by the current WeakReference object
        /// has been garbage collected; otherwise, a reference to the object referenced
        /// by the current WeakReference object.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The reference to the target object is invalid. This exception can be thrown
        /// while setting this property if the value is a null reference or if the object
        /// has been finalized during the set operation.
        /// </exception>
        public virtual T Target
        {
            get
            {
                return (T)Reference.Target;
            }
            set
            {
                Reference.Target = value;
            }
        }
           
        /// <summary>
        /// Gets an indication whether the object referenced by the current WeakReference
        /// object has been garbage collected.
        /// </summary>
        /// <returns>
        /// true if the object referenced by the current System.WeakReference object
        /// has not been garbage collected and is still accessible; otherwise, false.
        /// </returns>
        public virtual bool IsAlive { get { return Reference.IsAlive; } }

        /// <summary>
        /// Gets an indication whether the object referenced by the current WeakReference
        /// object is tracked after it is finalized.
        /// </summary>
        /// <returns>
        /// true if the object the current System.WeakReference object refers to is tracked
        /// after finalization; or false if the object is only tracked until finalization.
        /// </returns>
        public virtual bool TrackResurrection { get { return Reference.TrackResurrection; } }

        /// <summary>
        /// Initializes a new instance of the System.WeakReference class, referencing
        /// the specified object.
        /// </summary>
        /// <param name="target">The object to track or null.</param>
        public WeakReference(T target)
        {
            Reference = new WeakReference(target);
        }

        /// <summary>
        /// Initializes a new instance of the System.WeakReference class, referencing
        /// the specified object and using the specified resurrection tracking.
        /// </summary>
        /// <param name="target">An object to track.</param>
        /// <param name="trackResurrection">
        /// Indicates when to stop tracking the object. If true, the object is tracked
        /// after finalization; if false, the object is only tracked until finalization.
        /// </param>
        [SecuritySafeCritical]
        public WeakReference(T target, bool trackResurrection)
        {
            Reference = new WeakReference(target, trackResurrection);
        }

        public static implicit operator WeakReference(WeakReference<T> reference)
        {
            return reference.Reference;
        }

        public static explicit operator WeakReference<T>(WeakReference reference)
        {
            return new WeakReference<T>((T)reference.Target, reference.TrackResurrection);
        }
    }
}
