using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Extension methods for textures.
    /// </summary>
    public static class TextureExtensions
    {
        /// <summary>
        /// Maps the UV coordinates in the range [0..1] to pixel coordinates
        /// in the range [0..width-1] and [0..height-1].
        /// </summary>
        /// <param name="texture">The texture to get the pixel coordinates from.</param>
        /// <param name="u">The UV coordinate in the x-axis.</param>
        /// <param name="v">The UV coordinate in the y-axis.</param>
        /// <param name="px">The output pixel coordinate in the x-axis.</param>
        /// <param name="py">The output pixel coordinate in the y-axis.</param>
        public static void GetPixelCoordinates(this Texture2D texture, float u, float v, out int px, out int py)
        {
            px = (int)Mathf.Lerp(0, texture.width - 1, u);
            py = (int)Mathf.Lerp(0, texture.height - 1, v);
        }

        /// <summary>
        /// Maps the pixel coordinates in the range [0..width-1] and
        /// [0..height-1] to UV coordinates in the range [0..1].
        /// </summary>
        /// <param name="texture">The texture to get the UV coordinates from.</param>
        /// <param name="px">The pixel coordinate in the x-axis.</param>
        /// <param name="py">The pixel coordinate in the y-axis.</param>
        /// <param name="u">The output UV coordinate in the x-axis.</param>
        /// <param name="v">The output UV coordinate in the y-axis.</param>
        public static void GetUVCoordinates(this Texture2D texture, int px, int py, out float u, out float v)
        {
            u = Mathf.InverseLerp(0, texture.width - 1, px);
            v = Mathf.InverseLerp(0, texture.height - 1, py);
        }

        /// <summarys>
        /// Gets the pixel color at the specified UV coordinates.
        /// </summary>
        /// <param name="texture">The texture to sample from.</param>
        /// <param name="u">The UV coordinate in the x-axis.</param>
        /// <param name="v">The UV coordinate in the y-axis.</param>
        /// <returns>The pixel color.</returns>
        public static Color Sample(this Texture2D texture, float u, float v)
        {
            if (texture == null || texture.width == 0 || texture.height == 0) {
                return Color.clear;
            }

            texture.GetPixelCoordinates(u, v, out int px, out int py);
            return texture.GetPixel(px, py);
        }

        /// <summary>
        /// Gets the pixel color at the UV coordinates calculated from a point
        /// inside a rectangle.
        /// </summary>
        /// <param name="texture">The texture to sample from.</param>
        /// <param name="rect">The rectangle to sample from.</param>
        /// <param name="point">The point inside the rectangle.</param>
        /// <returns>The pixel color.</returns>
        public static Color Sample(this Texture2D texture, Rect rect, Vector2 point)
        {
            if (texture == null || texture.width == 0 || texture.height == 0) {
                return Color.clear;
            }

            float u = Mathf.InverseLerp(rect.min.x, rect.max.x, point.x);
            float v = Mathf.InverseLerp(rect.min.y, rect.max.y, point.y);

            return Sample(texture, u, v);
        }

        /// <summary>
        /// Gets the pixel color at the UV coordinates calculated from a
        /// position inside a bounds. The position uses the x and z axis.
        /// </summary>
        /// <param name="texture">The texture to sample from.</param>
        /// <param name="bounds">The bounds to sample from.</param>
        /// <param name="position">The position inside the bounds.</param>
        /// <returns>The pixel color.</returns>
        public static Color Sample(this Texture2D texture, Bounds bounds, Vector3 position)
        {
            if (texture == null || texture.width == 0 || texture.height == 0) {
                return Color.clear;
            }

            float u = Mathf.InverseLerp(bounds.min.x, bounds.max.x, position.x);
            float v = Mathf.InverseLerp(bounds.min.z, bounds.max.z, position.z);

            return Sample(texture, u, v);
        }

        /// <summary>
        /// Sets every pixel in the texture to the specified color.
        /// </summary>
        /// <param name="texture">The texture to set the color of.</param>
        /// <param name="color">The color to set the texture to.</param>
        public static void SetColor(this Texture2D texture, Color32 color)
        {
            var colors = texture.GetRawTextureData<Color32>();
            int length = colors.Length;

            for (int i = 0; i < length; i++) {
                colors[i] = color;
            }

            texture.Apply();
        }

    }

}
