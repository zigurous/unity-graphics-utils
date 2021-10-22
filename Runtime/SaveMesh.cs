using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Saves the mesh of a mesh filter into a project asset.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [AddComponentMenu("Zigurous/Graphics/Save Mesh")]
    public sealed class SaveMesh : MonoBehaviour
    {
        /// <summary>
        /// The name of the saved asset. The mesh name will be used if not set.
        /// </summary>
        [Tooltip("The name of the saved asset. The mesh name will be used if not set.")]
        public string assetName;

        /// <summary>
        /// Saves the mesh on start, otherwise it needs to be called manually.
        /// </summary>
        [Tooltip("Saves the mesh on start, otherwise it needs to be called manually.")]
        public bool saveOnStart = true;

        private void Reset()
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            if (Application.isPlaying)
            {
                if (filter.mesh != null) {
                    assetName = filter.mesh.name;
                }
            }
            else
            {
                if (filter.sharedMesh != null) {
                    assetName = filter.sharedMesh.name;
                }
            }
        }

        private void Start()
        {
            #if UNITY_EDITOR
            if (saveOnStart) {
                Save();
            }
            #endif
        }

        #if UNITY_EDITOR
        /// <summary>
        /// Saves the mesh as a project asset.
        /// </summary>
        public void Save()
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            if (assetName.Length > 0) {
                filter.SaveMesh(assetName);
            } else {
                filter.SaveMesh();
            }
        }
        #endif

    }

}
