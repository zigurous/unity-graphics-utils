using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Combines children meshes into one mesh.
    /// </summary>
    [AddComponentMenu("Zigurous/Graphics/Combine Children Meshes")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.graphics/api/Zigurous.Graphics/CombineChildrenMeshes")]
    [RequireComponent(typeof(MeshFilter))]
    public sealed class CombineChildrenMeshes : MonoBehaviour
    {
        /// <summary>
        /// The name of the combined mesh.
        /// </summary>
        [Tooltip("The name of the combined mesh.")]
        public string combinedMeshName = "Combined Mesh";

        /// <summary>
        /// Combines the mesh on start, otherwise it needs to be called manually.
        /// </summary>
        [Tooltip("Combines the mesh on start, otherwise it needs to be called manually.")]
        public bool combineOnStart = true;

        /// <summary>
        /// Destroys the child game objects after combining.
        /// </summary>
        [Tooltip("Destroys the child game objects after combining.")]
        public bool deleteChildren = true;

        /// <summary>
        /// Combines all of the meshes into a single submesh.
        /// </summary>
        [Tooltip("Combines all of the meshes into a single submesh.")]
        public bool mergeSubmeshes = true;

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

        private void Reset()
        {
            enabled = false;
        }

        private void Start()
        {
            MeshFilter parent = GetComponent<MeshFilter>();

            if (combineOnStart && parent != null) {
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
            CombineInstance[] combine = new CombineInstance[children.Length];

            int submesh = 0;

            for (int i = 0; i < children.Length; i++)
            {
                MeshFilter child = children[i];

                if (child.mesh == null) {
                    continue;
                }

                CombineInstance instance = new CombineInstance();
                instance.mesh = child.mesh;
                instance.transform = child.transform.localToWorldMatrix;
                combine[submesh++] = instance;

                if (child.transform != this.transform)
                {
                    if (deleteChildren) {
                        Destroy(child.gameObject);
                    } else {
                        child.gameObject.SetActive(false);
                    }
                }
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.name = combinedMeshName;
            combinedMesh.CombineMeshes(combine, mergeSubmeshes);

            if (optimizeMesh) {
                combinedMesh.Optimize();
            }

            if (recalculateBounds) {
                combinedMesh.RecalculateBounds();
            }

            return combinedMesh;
        }

    }

}
