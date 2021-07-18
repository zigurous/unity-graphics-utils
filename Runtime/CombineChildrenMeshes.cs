using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Combines the meshes of the children of the game object into one mesh.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [AddComponentMenu("Zigurous/Graphics/Combine Children Meshes")]
    public sealed class CombineChildrenMeshes : MonoBehaviour
    {
        /// <summary>
        /// Combines the mesh on start, otherwise it needs to be called manually.
        /// </summary>
        [Tooltip("Combines the mesh on start, otherwise it needs to be called manually.")]
        public bool combineOnStart = true;

        /// <summary>
        /// Removes the child meshes from the game object after combining.
        /// </summary>
        [Tooltip("Removes the child meshes from the game object after combining.")]
        public bool removeChildMeshes = true;

        /// <summary>
        /// Optimizes the combined mesh data to improve rendering performance.
        /// </summary>
        [Tooltip("Optimizes the combined mesh data to improve rendering performance.")]
        public bool optimizeMesh = true;

        /// <summary>
        /// Recalculates the bounding volume of the combined mesh.
        /// </summary>
        [Tooltip("Recalculates the bounding volume of the combined mesh.")]
        public bool recalculateBounds = true;

        private void Start()
        {
            MeshFilter parent = GetComponent<MeshFilter>();

            if (this.combineOnStart && parent != null) {
                parent.mesh = Combine();
            }
        }

        /// <summary>
        /// Combines the meshes of the children of this game object.
        /// </summary>
        /// <returns>A new combined mesh.</returns>
        public Mesh Combine()
        {
            MeshFilter[] children = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[children.Length - 1];

            int submesh = 0;

            for (int i = 0; i < children.Length; i++)
            {
                MeshFilter child = children[i];

                // Ignore the parent mesh
                if (child.transform == this.transform) {
                    continue;
                }

                // Create a mesh combine instance
                CombineInstance instance = new CombineInstance();
                instance.mesh = child.mesh;
                instance.transform = Matrix4x4.TRS(child.transform.localPosition, child.transform.localRotation, child.transform.localScale);
                combine[submesh++] = instance;

                // Destroy the child mesh
                if (this.removeChildMeshes)
                {
                    Destroy(child.GetComponent<MeshRenderer>());
                    Destroy(child);
                }
            }

            // Create a new mesh from all of the combined children
            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combine);

            if (this.optimizeMesh) {
                combinedMesh.Optimize();
            }

            if (this.recalculateBounds) {
                combinedMesh.RecalculateBounds();
            }

            return combinedMesh;
        }

    }

}
