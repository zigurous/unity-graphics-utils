using UnityEngine;
using UnityEngine.Rendering;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Utility class to generate procedural meshes at runtime.
    /// </summary>
    public static class MeshGenerator
    {
        /// <summary>
        /// A delegate function to calculate the vertex for a given point.
        /// </summary>
        /// <param name="x">The coordinate of the point along the x-axis.</param>
        /// <param name="y">The coordinate of the point along the y-axis.</param>
        /// <param name="u">The normalized coordinate of the point along the x-axis in the range [0..1].</param>
        /// <param name="v">The normalized coordinate of the point along the y-axis in the range [0..1].</param>
        /// <returns>The vertex for the given point.</returns>
        public delegate Vector3 VertexGenerator(int x, int y, float u, float v);

        /// <summary>
        /// Creates a new procedural mesh of a grid of points.
        /// </summary>
        /// <param name="width">The width of the grid of points.</param>
        /// <param name="height">The height of the grid of points.</param>
        public static Mesh Create(int width, int height)
        {
            Mesh mesh = new();

            if (width * height > 65535) {
                mesh.indexFormat = IndexFormat.UInt32;
            }

            mesh.vertices = CreateVertices(width, height, DefaultVertexGenerator);
            mesh.triangles = CreateTriangles(width, height);
            mesh.uv = CreateUVs(width, height);
            mesh.RecalculateNormals();

            return mesh;
        }

        /// <summary>
        /// Creates a new procedural mesh using a custom vertex function.
        /// </summary>
        /// <param name="width">The width of the grid of points.</param>
        /// <param name="height">The height of the grid of points.</param>
        /// <param name="vertexGenerator">A custom function to calculate the vertex for a given point.</param>
        public static Mesh Create(int width, int height, VertexGenerator vertexGenerator)
        {
            vertexGenerator ??= DefaultVertexGenerator;

            Mesh mesh = new();

            if (width * height > 65535) {
                mesh.indexFormat = IndexFormat.UInt32;
            }

            mesh.vertices = CreateVertices(width, height, vertexGenerator);
            mesh.triangles = CreateTriangles(width, height);
            mesh.uv = CreateUVs(width, height);
            mesh.RecalculateNormals();

            return mesh;
        }

        private static Vector3[] CreateVertices(int width, int height, VertexGenerator vertexGenerator)
        {
            Vector3[] verticies = new Vector3[(width + 1) * (height + 1)];

            int index = 0;

            for (int y = 0; y <= height; y++)
            {
                for (int x = 0; x <= width; x++)
                {
                    float u = x / (float)width;
                    float v = y / (float)height;

                    verticies[index] = vertexGenerator(x, y, u, v);
                    index++;
                }
            }

            return verticies;
        }

        private static int[] CreateTriangles(int width, int height)
        {
            int[] triangles = new int[width * height * 6];

            int vert = 0;
            int tris = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + width + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + width + 1;
                    triangles[tris + 5] = vert + width + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            return triangles;
        }

        private static Vector2[] CreateUVs(int width, int height)
        {
            Vector2[] uvs = new Vector2[(width + 1) * (height + 1)];

            int index = 0;

            for (int y = 0; y <= height; y++)
            {
                for (int x = 0; x <= width; x++)
                {
                    float u = x / (float)width;
                    float v = y / (float)height;

                    uvs[index] = new Vector2(u, v);
                    index++;
                }
            }

            return uvs;
        }

        private static Vector3 DefaultVertexGenerator(int x, int y, float u, float v)
        {
            return new Vector3(x, 0f, y);
        }

    }

}
