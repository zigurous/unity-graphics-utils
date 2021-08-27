using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Generates a new cube mesh with 3 submeshes (one for each axis) and
    /// applies it to the mesh filter.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(MeshFilter))]
    [AddComponentMenu("Zigurous/Graphics/Cube Mesh 3")]
    public sealed class CubeMesh3 : MonoBehaviour
    {
        /// <summary>
        /// The triangles of the cube for the faces in the x-axis (Read only).
        /// </summary>
        public static readonly int[] trianglesX = new int[] { 16, 17, 18, 16, 18, 19, 20, 21, 22, 20, 22, 23 };

        /// <summary>
        /// The triangles of the cube for the faces in the y-axis (Read only).
        /// </summary>
        public static readonly int[] trianglesY = new int[] { 8, 4, 5, 8, 5, 9, 12, 13, 14, 12, 14, 15, };

        /// <summary>
        /// The triangles of the cube for the faces in the z-axis (Read only).
        /// </summary>
        public static readonly int[] trianglesZ = new int[] { 0, 2, 3, 0, 3, 1, 10, 6, 7, 10, 7, 11 };

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
        /// Creates a new cube mesh with 3 submeshes (one for each axis).
        /// </summary>
        /// <returns>The new cube mesh.</returns>
        public Mesh Create()
        {
            Mesh mesh = CubeMesh.sharedMesh.Copy();
            mesh.name = "Cube-3";
            mesh.subMeshCount = 3;
            mesh.SetTriangles(trianglesX, 0);
            mesh.SetTriangles(trianglesY, 1);
            mesh.SetTriangles(trianglesZ, 2);
            return mesh;
        }

    }

}
