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
        private static Mesh m_SharedMesh;

        /// <summary>
        /// A cube mesh that can be shared across many objects (Read only).
        /// </summary>
        /// <returns>A shared cube mesh instance.</returns>
        public static Mesh sharedMesh
        {
            get
            {
                if (m_SharedMesh == null)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    if (Application.isPlaying) {
                        m_SharedMesh = cube.GetComponent<MeshFilter>().mesh.Copy();
                    } else {
                        m_SharedMesh = cube.GetComponent<MeshFilter>().sharedMesh.Copy();
                    }

                    DestroyImmediate(cube);
                }

                return m_SharedMesh;
            }
        }

        /// <summary>
        /// The triangles of the cube (Read only).
        /// </summary>
        public static readonly int[] triangles = {
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

        /// <summary>
        /// The corners of the cube (Read only).
        /// </summary>
        public static readonly Vector3[] corners = {
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
        };

        /// <summary>
        /// The verticies of the cube (Read only).
        /// </summary>
        public static readonly Vector3[] vertices = {
            corners[0], corners[1], corners[2], corners[3], // Front
            corners[4], corners[5], corners[6], corners[7], // Back
            corners[2], corners[3], corners[4], corners[5], // Top
            corners[6], corners[0], corners[1], corners[7], // Bottom
            corners[1], corners[3], corners[5], corners[7], // Left
            corners[6], corners[4], corners[2], corners[0], // Right
        };

        /// <summary>
        /// The UV coordinates of the cube (Read only).
        /// </summary>
        public static readonly Vector2[] uv = {
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
        /// Creates a new cube mesh.
        /// </summary>
        /// <returns>The new cube mesh.</returns>
        public Mesh Create()
        {
            Mesh mesh = CubeMesh.sharedMesh.Copy();
            mesh.name = "Cube";
            return mesh;
        }

    }

}
