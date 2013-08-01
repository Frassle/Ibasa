using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    /// <summary>
    /// Base class for all game components.
    /// </summary>
    public abstract class Component : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public Component(Game game)
        {
            Game = game;
            _UpdateOrder = 0;
            _Enabled = true;

            // Cache public instance methods which return void and have object as the first parameter
            Methods = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).
                Where(method => method.ReturnType == typeof(void) && method.GetParameters().Length >= 1 && method.GetParameters()[0].ParameterType == typeof(object)).
                ToArray();
        }

        /// <summary>
        /// Cache of public instance methods to be used in the message dispatch virtual lookup.
        /// </summary>
        private System.Reflection.MethodInfo[] Methods;

        /// <summary>
        /// The game this component is attached to.
        /// </summary>
        public Game Game
        {
            get;
            private set;
        }

        private bool _Enabled;
        /// <summary>
        /// Indicates whether the component should be updated.
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; OnEnabledChanged(this, new EventArgs()); }
        }

        private int _UpdateOrder;
        /// <summary>
        /// Indicates the order in which the Component should be updated relative
        /// to other Component instances. Lower values are updated first.
        /// </summary>
        public int UpdateOrder
        {
            get { return _UpdateOrder; }
            set { _UpdateOrder = value; OnUpdateOrderChanged(this, new EventArgs()); }
        }

        /// <summary>
        /// Raised when the Enabled property changes.
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged;

        /// <summary>
        /// Raised when the UpdateOrder property changes.
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;

        #region Dispose
        #region Dispose helper code
        /// <summary>
        /// Call this before every method that requires resources.
        /// </summary>
        void ObjectDisposedException()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName, string.Format("The {0} has been disposed.", GetType().FullName));
        }

        /// <summary>
        /// Occurs when Dispose is called.
        /// </summary>
        public event global::System.EventHandler Disposing;

#if DEBUG
        /// <summary>
        /// StackTrace from construction.
        /// </summary>
        private readonly global::System.Diagnostics.StackTrace Trace = new global::System.Diagnostics.StackTrace(true);
#endif

        /// <summary>
        /// Gets a value that indicates whether the object is disposed. 
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Object finalizer, writes debug message if called.
        /// </summary>
        ~Component()
        {
#if DEBUG
            string message = "!! Forgot to dispose a " + GetType().FullName;
            message += "\n\nStack at construction:\n\n" + Trace + "!!";
            global::System.Diagnostics.Debug.WriteLine(message);
#else

#endif
        }

        /// <summary>
        /// Immediately releases the resources used by this object.
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                try
                {
                    global::System.EventHandler handler = Disposing;
                    if (handler != null)
                    {
                        handler(this, null);
                    }
                    OnDispose();
                    IsDisposed = true;
                }
                finally
                {
                    global::System.GC.SuppressFinalize(this);
                }
            }
        }
        #endregion
        /// <summary>
        /// Immediately releases the resources used by this object. 
        /// </summary>
        protected virtual void OnDispose() { }
        #endregion

        /// <summary>
        /// Dispatches a message to this component.
        /// The base component type does dynamic dispatch.
        /// </summary>
        /// <param name="message">A message</param>
        public virtual bool Dispatch(Message message)
        {
            foreach (var method in Methods)
            {
                if (method.Name == message.Method)
                {
                    var parameters = method.GetParameters();
                    bool parametersMatch = false;

                    object[] args = new object[parameters.Length];
                    args[0] = message.Sender;
                    message.Args.CopyTo(args, 1);
                    var types = Type.GetTypeArray(args);

                    if (parameters[0].ParameterType == typeof(object))
                    {
                        parametersMatch = true;
                        int index;
                        for (index = 1; index < Math.Min(parameters.Length, args.Length); ++index)
                        {
                            if (parameters[index].ParameterType != types[index])
                            {
                                parametersMatch = false;
                                break;
                            }
                        }
                        for (; index < parameters.Length; ++index)
                        {
                            if (parameters[index].IsOptional)
                            {
                                args[index] = parameters[index].DefaultValue;
                            }
                            else
                            {
                                parametersMatch = false;
                                break;
                            }
                        }
                    }

                    if (parametersMatch)
                    {
                        method.Invoke(this, args);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Called when all properties and components are ready to be queried.
        /// </summary>
        public virtual void Initalize()
        {
        }

        /// <summary>
        /// Called when the Enabled property changes. Raises the EnabledChanged event.
        /// </summary>
        /// <param name="sender">The Component.</param>
        /// <param name="args">Arguments to the EnabledChanged event.</param>
        protected virtual void OnEnabledChanged(object sender, EventArgs args)
        {
            var handler = EnabledChanged;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        /// <summary>
        /// Called when the UpdateOrder property changes. Raises the UpdateOrderChanged event.
        /// </summary>
        /// <param name="sender">The Component.</param>
        /// <param name="args">Arguments to the UpdateOrderChanged event.</param>
        protected virtual void OnUpdateOrderChanged(object sender, EventArgs args)
        {
            var handler = UpdateOrderChanged;
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        /// <summary>
        /// Called when the GameComponent needs to be updated. 
        /// Called for each component sequentially based on UpdateOrder.
        /// </summary>
        /// <param name="elapsed">Fixed time step since the last update.</param>
        public virtual void FixedUpdate(GameTime time) { }

        /// <summary>
        /// Called when the GameComponent needs to be updated. 
        /// Called in parallel with all other systems after sequential FixedUpdate.
        /// </summary>
        /// <param name="elapsed">Fixed time step since the last update.</param>
        public virtual void ParallelUpdate(GameTime time) { }

        /// <summary>
        /// Called when the GameComponent needs to be updated.
        /// Called for each component sequentially based on after FixedUpdate and ParallelUpdate.
        /// </summary>
        /// <param name="elapsed">Variable time since last frame.</param>
        public virtual void Update(GameTime time)
        {
        }
    }
}
