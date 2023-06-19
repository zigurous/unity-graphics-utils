using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// The base class to draw a custom texture at runtime.
    /// </summary>
    public abstract class TextureDrawer : ScriptableObject
    {
        /// <summary>
        /// The drawn texture (Read only).
        /// </summary>
        public Texture2D texture { get; private set; }

        /// <summary>
        /// The width and height of the texture.
        /// </summary>
        [Header("Texture Settings")]
        [Tooltip("The width and height of the texture.")]
        public Vector2Int size = new Vector2Int(1024, 1024);

        /// <summary>
        /// The filter mode of the texture.
        /// </summary>
        [Tooltip("The filter mode of the texture.")]
        public FilterMode filterMode = FilterMode.Bilinear;

        /// <summary>
        /// The wrap mode of the texture.
        /// </summary>
        [Tooltip("The wrap mode of the texture.")]
        public TextureWrapMode wrapMode = TextureWrapMode.Clamp;

        /// <summary>
        /// Draws the texture.
        /// </summary>
        /// <returns>The drawn texture.</returns>
        public Texture2D Draw()
        {
            if (texture == null || texture.width != size.x || texture.height != size.y) {
                texture = new Texture2D(size.x, size.y);
            }

            texture.filterMode = filterMode;
            texture.wrapMode = wrapMode;

            SetPixels(texture);
            texture.Apply();

            return texture;
        }

        /// <summary>
        /// Sets the pixels of the texture.
        /// </summary>
        /// <param name="texture">The texture to set the pixels on.</param>
        public abstract void SetPixels(Texture2D texture);

    }

}
