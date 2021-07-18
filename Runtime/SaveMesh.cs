#if UNITY_EDITOR
using UnityEditor;
#endif
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
        /// The name the asset is saved with. The mesh name will be used if not
        /// set.
        /// </summary>
        [Tooltip("The name the asset is saved with. The mesh name will be used if not set.")]
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
                    this.assetName = filter.mesh.name;
                }
            }
            else
            {
                if (filter.sharedMesh != null) {
                    this.assetName = filter.sharedMesh.name;
                }
            }
        }

        private void Start()
        {
            if (this.saveOnStart) {
                Save();
            }
        }

        /// <summary>
        /// Saves the mesh to a project asset.
        /// </summary>
        public void Save()
        {
            #if UNITY_EDITOR
            MeshFilter filter = GetComponent<MeshFilter>();

            if (filter.mesh != null)
            {
                if (this.assetName.Length > 0) {
                    AssetDatabase.CreateAsset(filter.mesh, "Assets/" + this.assetName + ".mesh");
                } else {
                    AssetDatabase.CreateAsset(filter.mesh, "Assets/" + filter.mesh.name + ".mesh");
                }
            }
            #endif
        }

    }

}
