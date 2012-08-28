using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    public sealed class EntityCollection : IEnumerable<Entity>
    {
        HashSet<Entity> Entities = new HashSet<Entity>();
        Game Game;

        public event Action<Entity> EntityCreated;
        public event Action<Entity> EntityDestroyed;

        public EntityCollection()
        {
        }

        public int Count { get { return Entities.Count; } }

        public void Clear()
        {
            while (Entities.Count > 0)
            {
                Destroy(Entities.First());
            }
            Entities.Clear();
        }

        public bool Valid(Entity entity)
        {
            return Entities.Contains(entity);
        }

        long GlobalEntityGUID = 1;
        public Entity CreateGlobal()
        {
            Entity entity = new Entity(GlobalEntityGUID++);
            while (!Entities.Add(entity))
            {
                entity = new Entity(++GlobalEntityGUID);
            }

            var action = EntityCreated;
            if (action != null)
                action(entity);

            return entity;
        }

        long LocalEntityGUID = -1;
        public Entity CreateLocal()
        {
            Entity entity = new Entity(LocalEntityGUID--);
            while (!Entities.Add(entity))
            {
                entity = new Entity(--LocalEntityGUID);
            }

            var action = EntityCreated;
            if (action != null)
                action(entity);

            return entity;
        }

        public bool Destroy(Entity entity)
        {
            if (Entities.Remove(entity))
            {
                var action = EntityDestroyed;
                if (action != null)
                    action(entity);

                return true;
            }
            return false;
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
