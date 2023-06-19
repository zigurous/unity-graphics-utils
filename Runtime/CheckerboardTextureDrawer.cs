using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Draws a checkerboard pattern texture.
    /// </summary>
    [CreateAssetMenu(menuName = "Zigurous/Graphics/Checkerboard Texture Drawer")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.graphics/api/Zigurous.Graphics/CheckerboardTextureDrawer")]
    public sealed class CheckerboardTextureDrawer : TextureDrawer
    {
        /// <summary>
        /// The number of rows in the checkerboard.
        /// </summary>
        [Header("Checkerboard Settings")]
        [Tooltip("The number of rows in the checkerboard.")]
        public int rows = 4;

        /// <summary>
        /// The number of columns in the checkerboard.
        /// </summary>
        [Tooltip("The number of columns in the checkerboard.")]
        public int columns = 4;

        /// <summary>
        /// The first color of the checkerboard.
        /// </summary>
        [Tooltip("The first color of the checkerboard.")]
        public Color colorA = Color.white;

        /// <summary>
        /// The second color of the checkerboard.
        /// </summary>
        [Tooltip("The second color of the checkerboard.")]
        public Color colorB = Color.black;

        /// <inheritdoc/>
        public override void SetPixels(Texture2D texture)
        {
            int rectWidth = texture.width / columns;
            int rectHeight = texture.height / rows;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Color color = (row + col) % 2 == 0 ? colorA : colorB;

                    int positionX = col * rectWidth;
                    int positionY = row * rectHeight;

                    for (int x = positionX; x < positionX + rectWidth; x++)
                    {
                        for (int y = positionY; y < positionY + rectHeight; y++)
                        {
                            texture.SetPixel(x, y, color);
                        }
                    }
                }
            }
        }

    }

}
