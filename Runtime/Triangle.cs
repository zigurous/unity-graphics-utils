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

        /// <summary>
        /// The first vertex of the triangle.
        /// </summary>
        public Vector3 v1;

        /// <summary>
        /// The second vertex of the triangle.
        /// </summary>
        public Vector3 v2;

        /// <summary>
        /// The third vertex of the triangle.
        /// </summary>
        public Vector3 v3;

        /// <summary>
        /// Creates a new triangle with the specified verticies.
        /// </summary>
        /// <param name="v1">The first vertex of the triangle.</param>
        /// <param name="v2">The second vertex of the triangle.</param>
        /// <param name="v3">The third vertex of the triangle.</param>
        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        /// <summary>
        /// Gets or sets a vertex of the triangle at the given index.
        /// </summary>
        /// <param name="index">The index of the vertex.</param>
        public Vector3 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return v1;
                    case 1: return v2;
                    case 2: return v3;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: v1 = value; break;
                    case 1: v2 = value; break;
                    case 2: v3 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Determines if the triangle is equal to another triangle.
        /// </summary>
        /// <param name="other">The triangle to compare to.</param>
        /// <returns>True if the triangles are equal, false otherwise.</returns>
        public bool Equals(Triangle other)
        {
            return this.v1 == other.v1 &&
                   this.v2 == other.v2 &&
                   this.v3 == other.v3;
        }

        /// <summary>
        /// Determines if the triangle is equal to another triangle.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>True if the triangles are equal, false otherwise.</returns>
        public override bool Equals(object other)
        {
            if (other is Triangle triangle) {
                return Equals(triangle);
            } else {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code of the triangle.
        /// </summary>
        /// <returns>The hash code of the triangle.</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + v1.GetHashCode();
                hash = hash * 23 + v2.GetHashCode();
                hash = hash * 23 + v3.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Converts the triangle to a string.
        /// </summary>
        /// <returns>The string representation of the triangle.</returns>
        public override string ToString()
        {
            return $"{v1.ToString()} {v2.ToString()} {v3.ToString()}";
        }

        /// <summary>
        /// Determines if two triangles are equal.
        /// </summary>
        /// <param name="lhs">The first triangle to compare.</param>
        /// <param name="rhs">The second triangle to compare.</param>
        /// <returns>True if the triangles are equal, false otherwise.</returns>
        public static bool operator ==(Triangle lhs, Triangle rhs) => lhs.Equals(rhs);

        /// <summary>
        /// Determines if two triangles are not equal.
        /// </summary>
        /// <param name="lhs">The first triangle to compare.</param>
        /// <param name="rhs">The second triangle to compare.</param>
        /// <returns>True if the triangles are not equal, false otherwise.</returns>
        public static bool operator !=(Triangle lhs, Triangle rhs) => !lhs.Equals(rhs);

    }

}
