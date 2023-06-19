using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Generates a new cube mesh with 6 submeshes (one for each face) and
    /// applies it to the mesh filter.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Graphics/Cube Mesh 6")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.graphics/api/Zigurous.Graphics/CubeMesh6")]
    [RequireComponent(typeof(MeshFilter))]
    public sealed class CubeMesh6 : MonoBehaviour
    {
        /// <summary>
        /// The triangles of the cube for the right face (Read only).
        /// </summary>
        public static readonly int[] trianglesRight = new int[] { 20, 21, 22, 20, 22, 23 };

        /// <summary>
        /// The triangles of the cube for the left face (Read only).
        /// </summary>
        public static readonly int[] trianglesLeft = new int[] { 16, 17, 18, 16, 18, 19 };

        /// <summary>
        /// The triangles of the cube for the top face (Read only).
        /// </summary>
        public static readonly int[] trianglesTop = new int[] { 8, 4, 5, 8, 5, 9 };

        /// <summary>
        /// The triangles of the cube for the bottom face (Read only).
        /// </summary>
        public static readonly int[] trianglesBottom = new int[] { 12, 13, 14, 12, 14, 15 };

        /// <summary>
        /// The triangles of the cube for the front face (Read only).
        /// </summary>
        public static readonly int[] trianglesFront = new int[] { 0, 2, 3, 0, 3, 1 };

        /// <summary>
        /// The triangles of the cube for the back face (Read only).
        /// </summary>
        public static readonly int[] trianglesBack = new int[] { 10, 6, 7, 10, 7, 11 };

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
            mesh.SetTriangles(trianglesRight, 0);
            mesh.SetTriangles(trianglesLeft, 1);
            mesh.SetTriangles(trianglesTop, 2);
            mesh.SetTriangles(trianglesBottom, 3);
            mesh.SetTriangles(trianglesFront, 4);
            mesh.SetTriangles(trianglesBack, 5);
            return mesh;
        }

    }

}
