using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Generates a new cube mesh and applies it to the mesh filter.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(MeshFilter))]
    [AddComponentMenu("Zigurous/Graphics/Cube Mesh")]
    public sealed class CubeMesh : MonoBehaviour
    {
        public static readonly int[] Triangles = {
             0,  2,  3,
             0,  3,  1,
             8,  4,  5,
             8,  5,  9,
            10,  6,  7,
            10,  7, 11,
            12, 13, 14,
            12, 14, 15,
            16, 17, 18,
            16, 18, 19,
            20, 21, 22,
            20, 22, 23,
        };

        public static readonly Vector3[] Corners = {
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
        };

        public static readonly Vector3[] Vertices = {
            Corners[0], Corners[1], Corners[2], Corners[3], // Front
            Corners[4], Corners[5], Corners[6], Corners[7], // Back
            Corners[2], Corners[3], Corners[4], Corners[5], // Top
            Corners[6], Corners[0], Corners[1], Corners[7], // Bottom
            Corners[1], Corners[3], Corners[5], Corners[7], // Left
            Corners[6], Corners[4], Corners[2], Corners[0], // Right
        };

        public static readonly Vector2[] UVs = {
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1), // Front
            new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 1), new Vector2(1, 1), // Back
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 0), new Vector2(1, 0), // Top
            new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), // Bottom
            new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), // Left
            new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), // Right
        };

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
            mesh.name = "Cube";
            mesh.vertices = Vertices;
            mesh.triangles = Triangles;
            mesh.uv = UVs;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();
            return mesh;
        }

    }

}
