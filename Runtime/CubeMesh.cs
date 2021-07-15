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
        private static Mesh _sharedMesh;

        /// <summary>
        /// A cube mesh that can be shared across many objects.
        /// </summary>
        public static Mesh sharedMesh
        {
            get
            {
                if (_sharedMesh == null)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    if (Application.isPlaying) {
                        _sharedMesh = cube.GetComponent<MeshFilter>().mesh.Copy();
                    } else {
                        _sharedMesh = cube.GetComponent<MeshFilter>().sharedMesh.Copy();
                    }

                    DestroyImmediate(cube);
                }

                return _sharedMesh;
            }
        }

        /// <summary>
        /// The triangles of the cube.
        /// </summary>
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

        /// <summary>
        /// The corners of the cube.
        /// </summary>
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

        /// <summary>
        /// The verticies of the cube.
        /// </summary>
        public static readonly Vector3[] Vertices = {
            Corners[0], Corners[1], Corners[2], Corners[3], // Front
            Corners[4], Corners[5], Corners[6], Corners[7], // Back
            Corners[2], Corners[3], Corners[4], Corners[5], // Top
            Corners[6], Corners[0], Corners[1], Corners[7], // Bottom
            Corners[1], Corners[3], Corners[5], Corners[7], // Left
            Corners[6], Corners[4], Corners[2], Corners[0], // Right
        };

        /// <summary>
        /// The UV coordinates of the cube.
        /// </summary>
        public static readonly Vector2[] UV = {
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
