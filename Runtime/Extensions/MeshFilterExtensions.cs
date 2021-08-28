#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Zigurous.Graphics
{
    public static class MeshFilterExtensions
    {
        #if UNITY_EDITOR
        /// <summary>
        /// Saves the mesh of the filter as a project asset (Editor only).
        /// </summary>
        /// <param name="filter">The mesh filter to save the mesh of.</param>
        public static void SaveMesh(this MeshFilter filter)
        {
            if (filter != null && filter.mesh != null) {
                AssetDatabase.CreateAsset(filter.mesh, "Assets/" + filter.mesh.name + ".mesh");
            }
        }
        #endif

        #if UNITY_EDITOR
        /// <summary>
        /// Saves the mesh of the filter as a project asset (Editor only).
        /// </summary>
        /// <param name="filter">The mesh filter to save the mesh of.</param>
        /// <param name="assetName">The name to save the asset as.</param>
        public static void SaveMesh(this MeshFilter filter, string assetName)
        {
            if (filter != null && filter.mesh != null) {
                AssetDatabase.CreateAsset(filter.mesh, "Assets/" + assetName + ".mesh");
            }
        }
        #endif

    }

}
