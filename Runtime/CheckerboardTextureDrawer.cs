using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Draws a texture of a checkerboard pattern.
    /// </summary>
    [AddComponentMenu("Zigurous/Graphics/Checkerboard Texture Drawer")]
    public sealed class CheckerboardTextureDrawer : TextureDrawer
    {
        /// <summary>
        /// The checkerboard settings of a <see cref="CheckerboardTextureDrawer"/>.
        /// </summary>
        [System.Serializable]
        public struct CheckerboardSettings
        {
            /// <summary>
            /// The number of rows in the checkerboard.
            /// </summary>
            [Tooltip("The number of rows in the checkerboard.")]
            public int rows;

            /// <summary>
            /// The number of columns in the checkerboard.
            /// </summary>
            [Tooltip("The number of columns in the checkerboard.")]
            public int columns;

            /// <summary>
            /// The first color of the checkerboard.
            /// </summary>
            [Tooltip("The first color of the checkerboard.")]
            public Color colorA;

            /// <summary>
            /// The second color of the checkerboard.
            /// </summary>
            [Tooltip("The second color of the checkerboard.")]
            public Color colorB;

            /// <summary>
            /// Creates new checkerboard settings with the specified values.
            /// </summary>
            /// <param name="rows">The number of rows in the checkerboard.</param>
            /// <param name="columns">The number of columns in the checkerboard.</param>
            /// <param name="colorA">The first color of the checkerboard.</param>
            /// <param name="colorB">The second color of the checkerboard.</param>
            public CheckerboardSettings(int rows, int columns, Color colorA, Color colorB)
            {
                this.rows = rows;
                this.columns = columns;
                this.colorA = colorA;
                this.colorB = colorB;
            }

        }

        /// <summary>
        /// The checkerboard settings.
        /// </summary>
        [Tooltip("The checkerboard settings.")]
        public CheckerboardSettings checkerboard = new CheckerboardSettings(4, 4, Color.white, Color.black);

        /// <inheritdoc/>
        protected override void SetPixels(Texture2D texture)
        {
            int rectWidth = texture.width / this.checkerboard.columns;
            int rectHeight = texture.height / this.checkerboard.rows;

            for (int row = 0; row < this.checkerboard.rows; row++)
            {
                for (int col = 0; col < this.checkerboard.columns; col++)
                {
                    Color color = (row + col) % 2 == 0 ? this.checkerboard.colorA : this.checkerboard.colorB;

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

        protected override void OnValidate()
        {
            base.OnValidate();

            if (this.checkerboard.rows < 2) {
                this.checkerboard.rows = 2;
            }

            if (this.checkerboard.columns < 2) {
                this.checkerboard.columns = 2;
            }
        }

    }

}
