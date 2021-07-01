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
        public static readonly int[] Triangles_Right = new int[] { 20, 21, 22, 20, 22, 23 };
        public static readonly int[] Triangles_Left = new int[] { 16, 17, 18, 16, 18, 19 };
        public static readonly int[] Triangles_Top = new int[] { 8, 4, 5, 8, 5, 9 };
        public static readonly int[] Triangles_Bottom = new int[] { 12, 13, 14, 12, 14, 15 };
        public static readonly int[] Triangles_Front = new int[] { 0, 2, 3, 0, 3, 1 };
        public static readonly int[] Triangles_Back = new int[] { 10, 6, 7, 10, 7, 11 };

        private void Awake()
        {
            Apply();
        }

        public void Apply()
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            if (Application.isPlaying) {
                filter.mesh = Create();
            } else {
                filter.sharedMesh = Create();
            }
        }

        public Mesh Create()
        {
            Mesh mesh = CubeMesh.sharedMesh.Copy();
            mesh.name = "Cube-6";
            mesh.subMeshCount = 6;
            mesh.SetTriangles(Triangles_Right, 0);
            mesh.SetTriangles(Triangles_Left, 1);
            mesh.SetTriangles(Triangles_Top, 2);
            mesh.SetTriangles(Triangles_Bottom, 3);
            mesh.SetTriangles(Triangles_Front, 4);
            mesh.SetTriangles(Triangles_Back, 5);
            return mesh;
        }

    }

}
