using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Game
{
    /// <summary>
    /// An entity is a Game Unique ID.
    /// </summary>
    public struct Entity : IEquatable<Entity>
    {
        private long Uid;

        internal Entity(long uid)
        {
            Uid = uid;
        }

        public bool IsLocal
        {
            get
            {
                return Uid < 0;
            }
        }

        public bool IsGlobal
        {
            get
            {
                return Uid >= 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity)
                return Equals((Entity)obj);
            return false;
        }

        public bool Equals(Entity other)
        {
            return Equals(this, other);
        }

        public static bool Equals(Entity left, Entity right)
        {
            return left.Uid == right.Uid;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }

        public override string ToString()
        {
            return Uid.ToString();
        }
    }
}
