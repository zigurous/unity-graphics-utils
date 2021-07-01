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
        public static readonly int[] Triangles_X = new int[] { 16, 17, 18, 16, 18, 19, 20, 21, 22, 20, 22, 23 };
        public static readonly int[] Triangles_Y = new int[] { 8, 4, 5, 8, 5, 9, 12, 13, 14, 12, 14, 15, };
        public static readonly int[] Triangles_Z = new int[] { 0, 2, 3, 0, 3, 1, 10, 6, 7, 10, 7, 11 };

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
            mesh.name = "Cube-3";
            mesh.subMeshCount = 3;
            mesh.vertices = CubeMesh.Vertices;
            mesh.uv = CubeMesh.UVs;
            mesh.SetTriangles(Triangles_X, 0);
            mesh.SetTriangles(Triangles_Y, 1);
            mesh.SetTriangles(Triangles_Z, 2);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();
            return mesh;
        }

    }

}
