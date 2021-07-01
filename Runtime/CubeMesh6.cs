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
        public static readonly int[] Triangles_X_Pos = new int[] { 20, 21, 22, 20, 22, 23 };
        public static readonly int[] Triangles_X_Neg = new int[] { 16, 17, 18, 16, 18, 19 };
        public static readonly int[] Triangles_Y_Pos = new int[] { 8, 4, 5, 8, 5, 9 };
        public static readonly int[] Triangles_Y_Neg = new int[] { 12, 13, 14, 12, 14, 15 };
        public static readonly int[] Triangles_Z_Pos = new int[] { 0, 2, 3, 0, 3, 1 };
        public static readonly int[] Triangles_Z_Neg = new int[] { 10, 6, 7, 10, 7, 11 };

        private void Awake()
        {
            Apply();
        }

        public void Apply()
        {
            MeshFilter filter = GetComponent<MeshFilter>();
            filter.mesh = Create();
        }

        public Mesh Create()
        {
            Mesh mesh = new Mesh();
            mesh.name = "Cube-6";
            mesh.subMeshCount = 6;
            mesh.vertices = CubeMesh.Vertices;
            mesh.uv = CubeMesh.UVs;
            mesh.SetTriangles(Triangles_X_Pos, 0);
            mesh.SetTriangles(Triangles_X_Neg, 1);
            mesh.SetTriangles(Triangles_Y_Pos, 2);
            mesh.SetTriangles(Triangles_Y_Neg, 3);
            mesh.SetTriangles(Triangles_Z_Pos, 4);
            mesh.SetTriangles(Triangles_Z_Neg, 5);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();
            return mesh;
        }

    }

}
