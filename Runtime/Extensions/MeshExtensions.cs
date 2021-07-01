using UnityEngine;

namespace Zigurous.Graphics
{
    public static class MeshExtensions
    {
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

        public static void InvertNormals(this Mesh mesh)
        {
            mesh.normals = mesh.InvertedNormals();
        }

        public static Vector3[] InvertedNormals(this Mesh mesh)
        {
            Vector3[] normals = mesh.normals;

            for (int i = 0; i < normals.Length; i++) {
                normals[i] = -normals[i];
            }

            return normals;
        }

        public static void InvertTriangles(this Mesh mesh, bool calculateBounds = false)
        {
            for (int m = 0; m < mesh.subMeshCount; m++) {
                mesh.SetTriangles(mesh.InvertedTriangles(m), m, calculateBounds);
            }
        }

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

    }

}
