﻿using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Inverts the normals and triangles of the mesh so it renders inside-out.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [AddComponentMenu("Zigurous/Graphics/Invert Mesh")]
    public sealed class InvertMesh : MonoBehaviour
    {
        /// <summary>
        /// Whether the mesh is currently inverted (Read only).
        /// </summary>
        public bool inverted { get; private set; }

        private void OnEnable()
        {
            if (!inverted) {
                Invert();
            }
        }

        private void OnDisable()
        {
            if (inverted) {
                Invert();
            }
        }

        /// <summary>
        /// Inverts the normals and triangles of the mesh.
        /// </summary>
        public void Invert()
        {
            inverted = !inverted;

            MeshFilter filter = GetComponent<MeshFilter>();

            if (filter.mesh != null)
            {
                filter.mesh.InvertTriangles();
                filter.mesh.InvertNormals();
            }
        }

    }

}
