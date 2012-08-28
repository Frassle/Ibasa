using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    public abstract class Property
    {
        public string Name
        {
            get;
            private set;
        }

        public IGame Game
        {
            get;
            private set;
        }

        public Property(IGame game, string name)
        {
            Game = game;
            Name = name;
        }
    }

    public class Property<T> : Property, IEnumerable<KeyValuePair<Entity, T>>
    {
        Ibasa.Collections.ArrayMap<Entity, T> Frontbuffer;
        Ibasa.Collections.ArrayMap<Entity, T> Backbuffer;

        System.Collections.Concurrent.ConcurrentBag<KeyValuePair<Entity, T>> ToAdd;
        System.Collections.Concurrent.ConcurrentBag<Entity> ToRemove;

        bool Persistent;

        public Property(IGame game, string name, bool persistent)
            : base(game, name)
        {
            Persistent = persistent;

            Frontbuffer = new Collections.ArrayMap<Entity, T>();
            Backbuffer = new Collections.ArrayMap<Entity, T>();

            ToAdd = new System.Collections.Concurrent.ConcurrentBag<KeyValuePair<Entity, T>>();
            ToRemove = new System.Collections.Concurrent.ConcurrentBag<Entity>();
        }

        public T this[Entity entity]
        {
            get
            {
                return Game.IsParallel ? GetFrontbuffer(entity) : GetBackbuffer(entity);
            }
            set
            {
                SetBackbuffer(entity, value);
            }
        }

        public void Add(Entity entity, T value)
        {
            if (Game.IsParallel)
            {
                ToAdd.Add(new KeyValuePair<Entity, T>(entity, value));
            }
            else
            {
                Backbuffer.Add(entity, value);
                Frontbuffer.Add(entity, value);
            }
        }

        public void Remove(Entity entity)
        {
            if (Game.IsParallel)
            {
                ToRemove.Add(entity);
            }
            else
            {
                Backbuffer.Remove(entity);
                Frontbuffer.Remove(entity);
            }
        }

        public T GetFrontbuffer(Entity entity)
        {
            return Frontbuffer[entity];
        }

        public bool TryGetFrontbuffer(Entity entity, out T value)
        {
            return Frontbuffer.TryGetValue(entity, out value);
        }

        public void SetFrontbuffer(Entity entity, T value)
        {
            Frontbuffer[entity] = value;
        }

        public IEnumerator<KeyValuePair<Entity, T>> GetFrontbuffer()
        {
            return Frontbuffer.GetEnumerator();
        }

        public T GetBackbuffer(Entity entity)
        {
            return Backbuffer[entity];
        }

        public bool TryGetBackbuffer(Entity entity, out T value)
        {
            return Backbuffer.TryGetValue(entity, out value);
        }

        public void SetBackbuffer(Entity entity, T value)
        {
            Backbuffer[entity] = value;
        }

        public IEnumerator<KeyValuePair<Entity, T>> GetBackbuffer()
        {
            return Backbuffer.GetEnumerator();
        }

        public void Swap()
        {
            foreach (var item in ToRemove)
            {
                Backbuffer.Remove(item);
                Frontbuffer.Remove(item);
            }

            if (Persistent)
            {
                foreach (var item in Backbuffer)
                {
                    Frontbuffer[item.Key] = item.Value;
                }
            }
            else
            {
                var buffer = Frontbuffer;

                Frontbuffer = Backbuffer;
                Backbuffer = buffer;
            }

            foreach (var item in ToAdd)
            {
                Backbuffer.Add(item.Key, item.Value);
                Frontbuffer.Add(item.Key, item.Value);
            }
        }

        public IEnumerator<KeyValuePair<Entity, T>> GetEnumerator()
        {
            return GetFrontbuffer();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
