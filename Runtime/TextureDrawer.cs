using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Draws a custom texture at runtime.
    /// </summary>
    [ExecuteAlways]
    public abstract class TextureDrawer : MonoBehaviour
    {
        /// <summary>
        /// The texture settings of a <see cref="TextureDrawer"/>.
        /// </summary>
        [System.Serializable]
        public struct TextureSettings
        {
            /// <summary>
            /// The width and height of the texture.
            /// </summary>
            [Tooltip("The width and height of the texture.")]
            public Vector2Int size;

            /// <summary>
            /// The filter mode of the texture.
            /// </summary>
            [Tooltip("The filter mode of the texture.")]
            public FilterMode filterMode;

            /// <summary>
            /// The wrap mode of the texture.
            /// </summary>
            [Tooltip("The wrap mode of the texture.")]
            public TextureWrapMode wrapMode;

            /// <summary>
            /// Creates new texture settings with specified values.
            /// </summary>
            /// <param name="size">The size of the texture.</param>
            /// <param name="filterMode">The filter mode of the texture.</param>
            /// <param name="wrapMode">The wrap mode of the texture.</param>
            public TextureSettings(Vector2Int size, FilterMode filterMode, TextureWrapMode wrapMode)
            {
                this.size = size;
                this.filterMode = filterMode;
                this.wrapMode = wrapMode;
            }

        }

        /// <summary>
        /// The render settings of a <see cref="TextureDrawer"/>.
        /// </summary>
        [System.Serializable]
        public struct RenderSettings
        {
            /// <summary>
            /// The shader property that holds the texture.
            /// </summary>
            [Tooltip("The shader property that holds the texture.")]
            public ShaderProperty shaderTextureName;

            /// <summary>
            /// The amount of scaling to apply to the transform (as a multiplier).
            /// </summary>
            [Tooltip("The amount of scaling to apply to the transform (as a multiplier).")]
            public float scaleFactor;

            /// <summary>
            /// Scales the transform of the object to match the texture size.
            /// </summary>
            [Tooltip("Scales the transform of the object to match the texture size.")]
            public bool scaleTransform;

            #if UNITY_EDITOR
            /// <summary>
            /// Updates the texture while in the editor.
            /// </summary>
            [Tooltip("Updates the texture while in the editor.")]
            public bool updateInEditor;
            #endif

            /// <summary>
            /// Creates new render settings with the specified values.
            /// </summary>
            /// <param name="shaderTextureName">The shader property that holds the texture.</param>
            /// <param name="scaleFactor">The amount of scaling to apply to the transform (as a multiplier).</param>
            /// <param name="scaleTransform">Scales the transform of the object to match the texture size.</param>
            public RenderSettings(ShaderProperty shaderTextureName, float scaleFactor = 1f, bool scaleTransform = false)
            {
                this.shaderTextureName = shaderTextureName;
                this.scaleFactor = scaleFactor;
                this.scaleTransform = scaleTransform;

                #if UNITY_EDITOR
                this.updateInEditor = false;
                #endif
            }

        }

        /// <summary>
        /// The drawn texture (Read only).
        /// </summary>
        public Texture2D texture { get; private set; }

        /// <summary>
        /// The texture settings.
        /// </summary>
        [Tooltip("The texture settings.")]
        public TextureSettings textureSettings = new TextureSettings(new Vector2Int(1024, 1024), FilterMode.Bilinear, TextureWrapMode.Clamp);

        /// <summary>
        /// The renderer component that holds the material the texture is added
        /// to (Read only).
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// The render settings.
        /// </summary>
        [Tooltip("The render settings.")]
        public RenderSettings renderSettings = new RenderSettings("_MainTex", 1f, false);

        /// <summary>
        /// Whether the settings have changed since the texture was last drawn
        /// (Read only).
        /// </summary>
        public bool invalidated { get; internal set; }

        /// <summary>
        /// A Unity lifecycle method called when the behavior is initialized.
        /// </summary>
        protected virtual void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// A Unity lifecycle method called when the behavior is enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            if (Application.isPlaying) {
                Draw();
            }
        }

        /// <summary>
        /// A Unity lifecycle method called during editor validation.
        /// </summary>
        protected virtual void OnValidate()
        {
            invalidated = true;

            if (textureSettings.size.x < 1) {
                textureSettings.size.x = 1;
            }

            if (textureSettings.size.y < 1) {
                textureSettings.size.y = 1;
            }

            if (renderer == null) {
                renderer = GetComponent<Renderer>();
            }
        }

        private void Update()
        {
            #if UNITY_EDITOR
            if (!(Application.isPlaying || renderSettings.updateInEditor)) {
                return;
            }
            #endif

            if (invalidated) {
                Draw();
            }
        }

        /// <summary>
        /// Draws the texture.
        /// </summary>
        /// <returns>The drawn texture.</returns>
        public Texture2D Draw()
        {
            if (texture == null || texture.width != textureSettings.size.x || texture.height != textureSettings.size.y) {
                texture = new Texture2D(textureSettings.size.x, textureSettings.size.y);
            }

            texture.filterMode = textureSettings.filterMode;
            texture.wrapMode = textureSettings.wrapMode;
            invalidated = false;

            SetPixels(texture);

            texture.Apply();

            ApplyTexture();
            SetTransformScale();

            return texture;
        }

        /// <summary>
        /// Applies the texture to the renderer material.
        /// </summary>
        private void ApplyTexture()
        {
            if (renderer == null) {
                return;
            }

            if (Application.isPlaying) {
                renderer.material.SetTexture(renderSettings.shaderTextureName.id, texture);
            } else {
                renderer.sharedMaterial = new Material(renderer.sharedMaterial);
                renderer.sharedMaterial.SetTexture(renderSettings.shaderTextureName.id, texture);
            }
        }

        /// <summary>
        /// Sets the scale of the transform based on the texture size.
        /// </summary>
        private void SetTransformScale()
        {
            if (texture == null || !renderSettings.scaleTransform) {
                return;
            }

            Vector3 scale = new Vector2(texture.width, texture.height);
            scale *= renderSettings.scaleFactor;
            scale.z = 1f;

            if (scale.x != Mathf.Infinity && scale.y != Mathf.Infinity) {
                transform.localScale = scale;
            }
        }

        /// <summary>
        /// Sets the pixels of the texture.
        /// </summary>
        /// <param name="texture">The texture to set the pixels on.</param>
        public abstract void SetPixels(Texture2D texture);

    }

}
