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
        KeyValuePair<Entity, T>[] Frontbuffer;
        KeyValuePair<Entity, T>[] Backbuffer;

        Dictionary<Entity, int> Indices;

        System.Collections.Concurrent.ConcurrentBag<KeyValuePair<Entity, T>> ToAdd;
        System.Collections.Concurrent.ConcurrentBag<Entity> ToRemove;

        public Property(IGame game, string name)
            : base(game, name)
        {
            Frontbuffer = new KeyValuePair<Entity, T>[10];
            Backbuffer = new KeyValuePair<Entity, T>[10];
            Indices = new Dictionary<Entity, int>();

            ToAdd = new System.Collections.Concurrent.ConcurrentBag<KeyValuePair<Entity, T>>();
            ToRemove = new System.Collections.Concurrent.ConcurrentBag<Entity>();
        }

        public int Count
        {
            get { return Indices.Count; }
        }

        public int Capacity
        {
            get
            {
                return Frontbuffer.Length;
            }
            set
            {
                value = Math.Max(value, Capacity);
                Array.Resize(ref Frontbuffer, value);
                Array.Resize(ref Backbuffer, value);
            }
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
                if (!Game.IsParallel)
                {
                    SetFrontbuffer(entity, value);
                }
            }
        }

        public void Add(Entity entity, T value)
        {
            var pair = new KeyValuePair<Entity, T>(entity, value);
            if (Game.IsParallel)
            {
                ToAdd.Add(pair);
            }
            else
            {
                if (Capacity == Count)
                    Capacity = (Capacity * 3) / 2; // *= 1.5

                Frontbuffer[Count] = pair;
                Backbuffer[Count] = pair;
                Indices.Add(entity, Count);
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
                var index = Indices[entity];
                var swap = Frontbuffer[Count - 1].Key;

                Frontbuffer[index] = Frontbuffer[Count - 1];
                Backbuffer[index] = Backbuffer[Count - 1];

                Indices[swap] = index;
                Indices.Remove(entity);
            }
        }

        public T GetFrontbuffer(Entity entity)
        {
            int index = Indices[entity];
            return Frontbuffer[index].Value;
        }

        public bool TryGetFrontbuffer(Entity entity, out T value)
        {
            int index;
            if (Indices.TryGetValue(entity, out index))
            {
                value = Frontbuffer[index].Value;
                return true;
            }
            value = default(T);
            return false;
        }

        public void SetFrontbuffer(Entity entity, T value)
        {
            int index = Indices[entity];
            Frontbuffer[index] = new KeyValuePair<Entity, T>(entity, value);
        }

        public IEnumerator<KeyValuePair<Entity, T>> GetFrontbuffer()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return Frontbuffer[i];
            }
        }

        public T GetBackbuffer(Entity entity)
        {
            int index = Indices[entity];
            return Backbuffer[index].Value;
        }

        public bool TryGetBackbuffer(Entity entity, out T value)
        {
            int index;
            if (Indices.TryGetValue(entity, out index))
            {
                value = Backbuffer[index].Value;
                return true;
            }
            value = default(T);
            return false;
        }

        public void SetBackbuffer(Entity entity, T value)
        {
            int index = Indices[entity];
            Backbuffer[index] = new KeyValuePair<Entity, T>(entity, value);
        }

        public IEnumerator<KeyValuePair<Entity, T>> GetBackbuffer()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return Backbuffer[i];
            }
        }

        public void Swap()
        {
            if (Game.IsParallel)
                throw new Exception("Tryed to swap in a parallel context.");

            var buffer = Frontbuffer;

            Frontbuffer = Backbuffer;
            Backbuffer = buffer;

            Entity remove;
            while(ToRemove.TryTake(out remove))
            {
                Remove(remove);
            }
            KeyValuePair<Entity, T> pair;
            while (ToAdd.TryTake(out pair))
            {
                Add(pair.Key, pair.Value);
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
