#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Zigurous.Graphics
{
    public static class MeshFilterExtensions
    {
        /// <summary>
        /// Saves the mesh of the filter as a project asset (Editor only).
        /// </summary>
        /// <param name="filter">The mesh filter to save the mesh of.</param>
        public static void SaveMesh(this MeshFilter filter)
        {
            #if UNITY_EDITOR
            if (filter != null)
            {
                if (Application.isPlaying) {
                    AssetDatabase.CreateAsset(filter.mesh, $"Assets/{filter.mesh.name}.mesh");
                } else {
                    AssetDatabase.CreateAsset(filter.sharedMesh, $"Assets/{filter.mesh.name}.mesh");
                }
            }
            #endif
        }

        /// <summary>
        /// Saves the mesh of the filter as a project asset (Editor only).
        /// </summary>
        /// <param name="filter">The mesh filter to save the mesh of.</param>
        /// <param name="assetName">The name to save the asset as.</param>
        public static void SaveMesh(this MeshFilter filter, string assetName)
        {
            #if UNITY_EDITOR
            if (filter != null)
            {
                if (Application.isPlaying) {
                    AssetDatabase.CreateAsset(filter.mesh, $"Assets/{assetName}.mesh");
                } else {
                    AssetDatabase.CreateAsset(filter.sharedMesh, $"Assets/{assetName}.mesh");
                }
            }
            #endif
        }

        /// <summary>
        /// Combines the meshes of the mesh filters into one mesh.
        /// </summary>
        /// <param name="filters">The mesh filters to combine.</param>
        /// <param name="optimizeMesh">Optimizes the combined mesh data to improve rendering performance.</param>
        /// <param name="recalculateBounds">Recalculates the bounding volume of the combined mesh.</param>
        /// <returns>The combined mesh.</returns>
        public static Mesh CombineMeshes(this MeshFilter[] filters, bool optimizeMesh = true, bool recalculateBounds = true)
        {
            CombineInstance[] combine = new CombineInstance[filters.Length];

            for (int i = 0; i < filters.Length; i++)
            {
                MeshFilter child = filters[i];
                CombineInstance instance = new CombineInstance();
                instance.mesh = child.mesh;
                instance.transform = Matrix4x4.TRS(child.transform.localPosition, child.transform.localRotation, child.transform.localScale);
                combine[i] = instance;
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.name = "Combined Mesh";
            combinedMesh.CombineMeshes(combine);

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
