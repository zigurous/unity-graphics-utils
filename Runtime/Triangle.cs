using System;
using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// A data structure of a mesh triangle comprised of 3 verticies.
    /// </summary>
    [System.Serializable]
    public struct Triangle : IEquatable<Triangle>
    {
        #pragma warning disable 649 // disable unassigned variable warning

        public Vector3 a;
        public Vector3 b;
        public Vector3 c;

        public Vector3 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return a;
                    case 1:
                        return b;
                    default:
                        return c;
                }
            }
        }

        public bool Equals(Triangle other)
        {
            return this.a == other.a &&
                   this.b == other.b &&
                   this.c == other.c;
        }

        public override bool Equals(object obj)
        {
            if (obj is Triangle triangle) {
                return Equals(triangle);
            } else {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + this.a.GetHashCode();
                hash = hash * 23 + this.b.GetHashCode();
                hash = hash * 23 + this.c.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Triangle lhs, Triangle rhs) => lhs.Equals(rhs);
        public static bool operator !=(Triangle lhs, Triangle rhs) => !lhs.Equals(rhs);

    }

}
