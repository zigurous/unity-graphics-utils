using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Generates a new cube mesh with 6 submeshes (one for each face) and
    /// applies it to the mesh filter.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(MeshFilter))]
    [AddComponentMenu("Zigurous/Graphics/Cube Mesh 6")]
    public sealed class CubeMesh6 : MonoBehaviour
    {
        /// <summary>
        /// The triangles of the cube for the right face.
        /// </summary>
        public static readonly int[] TrianglesRight = new int[] { 20, 21, 22, 20, 22, 23 };

        /// <summary>
        /// The triangles of the cube for the left face.
        /// </summary>
        public static readonly int[] TrianglesLeft = new int[] { 16, 17, 18, 16, 18, 19 };

        /// <summary>
        /// The triangles of the cube for the top face.
        /// </summary>
        public static readonly int[] TrianglesTop = new int[] { 8, 4, 5, 8, 5, 9 };

        /// <summary>
        /// The triangles of the cube for the bottom face.
        /// </summary>
        public static readonly int[] TrianglesBottom = new int[] { 12, 13, 14, 12, 14, 15 };

        /// <summary>
        /// The triangles of the cube for the front face.
        /// </summary>
        public static readonly int[] TrianglesFront = new int[] { 0, 2, 3, 0, 3, 1 };

        /// <summary>
        /// The triangles of the cube for the back face.
        /// </summary>
        public static readonly int[] TrianglesBack = new int[] { 10, 6, 7, 10, 7, 11 };

        private void Awake()
        {
            Apply();
        }

        /// <summary>
        /// Applies a new cube mesh to the mesh filter.
        /// </summary>
        public void Apply()
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            if (Application.isPlaying) {
                filter.mesh = Create();
            } else {
                filter.sharedMesh = Create();
            }
        }

        /// <summary>
        /// Creates a new cube mesh with 6 submeshes (one for each face).
        /// </summary>
        /// <returns>The new cube mesh.</returns>
        public Mesh Create()
        {
            Mesh mesh = CubeMesh.sharedMesh.Copy();
            mesh.name = "Cube-6";
            mesh.subMeshCount = 6;
            mesh.SetTriangles(TrianglesRight, 0);
            mesh.SetTriangles(TrianglesLeft, 1);
            mesh.SetTriangles(TrianglesTop, 2);
            mesh.SetTriangles(TrianglesBottom, 3);
            mesh.SetTriangles(TrianglesFront, 4);
            mesh.SetTriangles(TrianglesBack, 5);
            return mesh;
        }

    }

}
