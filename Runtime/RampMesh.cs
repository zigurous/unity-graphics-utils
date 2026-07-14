using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Generates a new ramp mesh and applies it to the mesh filter.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Graphics/Ramp Mesh")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.graphics/api/Zigurous.Graphics/RampMesh")]
    [RequireComponent(typeof(MeshFilter))]
    public sealed class RampMesh : MonoBehaviour
    {
        public float length = 1f;

        private void Awake()
        {
            Apply();
        }

        private void OnValidate()
        {
            Apply();
        }

        /// <summary>
        /// Applies a new ramp mesh to the mesh filter.
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
        /// Creates a new ramp mesh.
        /// </summary>
        /// <returns>The new ramp mesh.</returns>
        public Mesh Create()
        {
            Vector3[] corners = {
                new Vector3( 0.5f, -0.5f,  0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3( 0.5f,  0.5f,  0.5f) - (Vector3.up * (1f - length)),
                new Vector3(-0.5f,  0.5f,  0.5f) - (Vector3.up * (1f - length)),
                new Vector3( 0.5f,  0.5f, -0.5f) +  Vector3.down,
                new Vector3(-0.5f,  0.5f, -0.5f) +  Vector3.down,
                new Vector3( 0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
            };

            Mesh mesh = CubeMesh.shared.Copy();
            mesh.subMeshCount = 5;
            mesh.vertices = new Vector3[] {
                corners[0], corners[1], corners[2], corners[3], // Front
                corners[4], corners[5], corners[6], corners[7], // Back
                corners[2], corners[3], corners[4], corners[5], // Top
                corners[6], corners[0], corners[1], corners[7], // Bottom
                corners[1], corners[3], corners[5], corners[7], // Left
                corners[6], corners[4], corners[2], corners[0], // Right
            };

            mesh.SetTriangles(CubeMesh6.trianglesRight, 0);
            mesh.SetTriangles(CubeMesh6.trianglesLeft, 1);
            mesh.SetTriangles(CubeMesh6.trianglesTop, 2);
            mesh.SetTriangles(CubeMesh6.trianglesBottom, 3);
            mesh.SetTriangles(CubeMesh6.trianglesFront, 4);

            return mesh;
        }

    }

}
