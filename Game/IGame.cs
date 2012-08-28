using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    public interface IGame
    {
        bool IsParallel { get; }

        void Post(Message message);

        Property<T> GetProperty<T>(string name);

        T GetComponent<T>() where T : Component;

        Entity CreateLocalEntity();
        Entity CreateGlobalEntity();

        void DestroyEntity(Entity entity);
    }
}
