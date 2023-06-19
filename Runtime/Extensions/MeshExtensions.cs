#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Extension methods for meshes.
    /// </summary>
    public static class MeshExtensions
    {
        /// <summary>
        /// Saves the mesh as a project asset (Editor only).
        /// </summary>
        /// <param name="mesh">The mesh to save.</param>
        /// <param name="assetName">The name to save the asset as.</param>
        public static void Save(this Mesh mesh, string assetName)
        {
            #if UNITY_EDITOR
            if (mesh != null) {
                AssetDatabase.CreateAsset(mesh, $"Assets/{assetName}.mesh");
            }
            #endif
        }

        /// <summary>
        /// Returns a new copy of the mesh.
        /// </summary>
        /// <param name="mesh">The mesh to copy.</param>
        /// <returns>A new copy of the mesh.</returns>
        public static Mesh Copy(this Mesh mesh)
        {
            Mesh copy = new Mesh();

            copy.indexFormat = mesh.indexFormat;
            copy.subMeshCount = mesh.subMeshCount;
            copy.vertices = mesh.vertices;

            for (int m = 0; m < mesh.subMeshCount; m++) {
                copy.SetTriangles(mesh.GetTriangles(m), m);
            }

            copy.normals = mesh.normals;
            copy.tangents = mesh.tangents;
            copy.uv = mesh.uv;
            copy.uv2 = mesh.uv2;
            copy.uv3 = mesh.uv3;
            copy.uv4 = mesh.uv4;
            copy.uv5 = mesh.uv5;
            copy.uv6 = mesh.uv6;
            copy.uv7 = mesh.uv7;
            copy.uv8 = mesh.uv8;
            copy.colors = mesh.colors;
            copy.bindposes = mesh.bindposes;
            copy.boneWeights = mesh.boneWeights;
            copy.bounds = mesh.bounds;

            return copy;
        }

        /// <summary>
        /// Inverts the normals of the mesh.
        /// </summary>
        /// <param name="mesh">The mesh to invert.</param>
        public static void InvertNormals(this Mesh mesh)
        {
            mesh.normals = mesh.InvertedNormals();
        }

        /// <summary>
        /// Returns an array of inverted normals of the mesh.
        /// </summary>
        /// <param name="mesh">The mesh to get the inverted normals from.</param>
        /// <returns>An array of inverted normals of the mesh.</returns>
        public static Vector3[] InvertedNormals(this Mesh mesh)
        {
            Vector3[] normals = mesh.normals;

            for (int i = 0; i < normals.Length; i++) {
                normals[i] = -normals[i];
            }

            return normals;
        }

        /// <summary>
        /// Inverts the triangles of the mesh.
        /// </summary>
        /// <param name="mesh">The mesh to invert.</param>
        /// <param name="calculateBounds">Recalculates the bounds of the mesh after inversion.</param>
        public static void InvertTriangles(this Mesh mesh, bool calculateBounds = false)
        {
            for (int m = 0; m < mesh.subMeshCount; m++) {
                mesh.SetTriangles(mesh.InvertedTriangles(m), m, calculateBounds);
            }
        }

        /// <summary>
        /// Returns an array of inverted triangles of the mesh.
        /// </summary>
        /// <param name="mesh">The mesh to get the inverted triangles from.</param>
        /// <returns>An array of inverted triangles of the mesh.</returns>
        public static int[] InvertedTriangles(this Mesh mesh)
        {
            int[] triangles = mesh.triangles;

            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }

            return triangles;
        }

        /// <summary>
        /// Returns an array of inverted triangles of a submesh of the mesh.
        /// </summary>
        /// <param name="mesh">The mesh to get the inverted triangles from.</param>
        /// <param name="submesh">The submesh index.</param>
        /// <returns>An array of inverted triangles of the submesh.</returns>
        public static int[] InvertedTriangles(this Mesh mesh, int submesh)
        {
            int[] triangles = mesh.GetTriangles(submesh);

            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }

            return triangles;
        }

        /// <summary>
        /// Calculates and assigns the UV coordinates of the mesh using its verticies.
        /// </summary>
        /// <param name="mesh">The mesh to calculate and assign the UV coordinates to.</param>
        public static void RecalculateUV(this Mesh mesh)
        {
            Bounds bounds = mesh.bounds;

            Vector3[] verticies = mesh.vertices;
            Vector2[] uvs = new Vector2[verticies.Length];

            for (int i = 0; i < verticies.Length; i++) {
                uvs[i] = new Vector2(verticies[i].x / bounds.size.x, verticies[i].y / bounds.size.y);
            }

            mesh.uv = uvs;
        }

    }

}
