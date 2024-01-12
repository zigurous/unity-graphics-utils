#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Saves the mesh of a mesh filter as a project asset.
    /// </summary>
    [AddComponentMenu("Zigurous/Graphics/Save Mesh")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.graphics/api/Zigurous.Graphics/SaveMesh")]
    [RequireComponent(typeof(MeshFilter))]
    public sealed class SaveMesh : MonoBehaviour
    {
        /// <summary>
        /// The name of the saved asset. The mesh name will be used if not set.
        /// </summary>
        [Tooltip("The name of the saved asset. The mesh name will be used if not set.")]
        public string assetName = "New Mesh";

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

            enabled = false;
        }

        private void Start()
        {
            if (saveOnStart) {
                Save();
            }
        }

        /// <summary>
        /// Saves the mesh as a project asset.
        /// </summary>
        public void Save()
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            if (string.IsNullOrEmpty(assetName)) {
                filter.SaveMesh();
            } else {
                filter.SaveMesh(assetName);
            }
        }

        #if UNITY_EDITOR
        [MenuItem("CONTEXT/SaveMesh/Save Mesh")]
        private static void ContextMenu_Save()
        {
            if (Selection.activeGameObject != null)
            {
                if (Selection.activeGameObject.TryGetComponent(out SaveMesh mesh)) {
                    mesh.Save();
                }
            }
        }
        #endif

    }

}
