using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Ibasa.Game
{
    public abstract class Game
    {
        private Time _Time = new Time();
        public Time Time { get { return _Time; } }
        
        public TimeSpan InactiveSleepTime { get; set; }
        public bool IsActive { get; set; }

        List<Component> Components { get; set; }
        List<Property> Properties { get; set; }
        System.Collections.Concurrent.ConcurrentQueue<Message> MessageQueue { get; set; }
        EntityCollection Entities { get; set; }

        internal bool IsParallel { get; private set; }

        public Game()
        {
            InactiveSleepTime = TimeSpan.FromSeconds(1.0);
            IsActive = true;

            Components = new List<Component>();
            Properties = new List<Property>();
            MessageQueue = new System.Collections.Concurrent.ConcurrentQueue<Message>();
            Entities = new EntityCollection();

            IsParallel = false;
        }

        public void AddComponent(Component component)
        {
            Components.Add(component);
            Components.Sort((a, b) => a.UpdateOrder - b.UpdateOrder);
            component.UpdateOrderChanged += new EventHandler<EventArgs>(UpdateOrderChanged);
        }

        void UpdateOrderChanged(object sender, EventArgs e)
        {
            Components.Sort((a, b) => a.UpdateOrder - b.UpdateOrder);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var component in Components)
            {
                var result = component as T;
                if (result != null)
                    return result;
            }
            return null;
        }

        public void Post(Message message)
        {
            MessageQueue.Enqueue(message);
        }

        public Property<T> AddProperty<T>(string name)
        {
            var property = new Property<T>(this, name);
            Properties.Add(property);
            return property;
        }

        public Property<T> GetProperty<T>(string name)
        {
            var property = Properties.Find(prop => prop.Name == name);
            if (property == null)
            {
                throw new KeyNotFoundException(string.Format("Property {0} not found", name));
            }

            var propertyT = property as Property<T>;
            if (propertyT == null)
            {
                throw new InvalidCastException(string.Format("Property {0} is not of type {1}", name, typeof(T).Name));
            }

            return propertyT;
        }

        public Entity CreateGlobalEntity()
        {
            return Entities.CreateGlobal();
        }

        public Entity CreateLocalEntity()
        {
            return Entities.CreateLocal();
        }

        public void DestroyEntity(Entity entity)
        {
            Entities.Destroy(entity);
            foreach (var property in Properties)
            {
                property.Remove(entity);
            }
        }

        private bool _initalized = false;
        public virtual void Initalize()
        {
            foreach (var component in Components)
            {
                component.Initalize();
            }

            Time.Start();
            _initalized = true;
        }

        public void Tick()
        {
            if (!_initalized)
                throw new System.InvalidOperationException("Initalize not called.");

            if (!IsActive)
                System.Threading.Thread.Sleep(InactiveSleepTime);

            Time.Tick(FixedUpdate, Update);
        }

        protected virtual void FixedUpdate(GameTime time)
        {
            foreach (var component in Components)
            {
                component.FixedUpdate(time);
            }
            
            IsParallel = true;

            Parallel.ForEach(Components, component => component.ParallelUpdate(time));

            Message message;
            while (MessageQueue.TryDequeue(out message))
            {
                int responses = 0;
                Parallel.ForEach(Components, component =>
                {
                    if (component.Dispatch(message))
                    {
                        System.Threading.Interlocked.Increment(ref responses);
                    }
                });

                if (responses == 0)
                {
                    Console.WriteLine("No response to message {0}", message.Method);
                }
            }

            IsParallel = false;

            Parallel.ForEach(Properties, property => property.Swap());
        }

        protected virtual void Update(GameTime time)
        {
            foreach (var component in Components)
            {
                component.Update(time);
            } 
        }
    }
}
