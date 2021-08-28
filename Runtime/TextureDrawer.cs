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

        protected virtual void Awake()
        {
            this.renderer = GetComponent<Renderer>();
        }

        protected virtual void OnValidate()
        {
            this.invalidated = true;

            if (this.textureSettings.size.x < 1) {
                this.textureSettings.size.x = 1;
            }

            if (this.textureSettings.size.y < 1) {
                this.textureSettings.size.y = 1;
            }

            if (this.renderer == null) {
                this.renderer = GetComponent<Renderer>();
            }
        }

        protected virtual void OnEnable()
        {
            if (Application.isPlaying) {
                Draw();
            }
        }

        protected virtual void Update()
        {
            #if UNITY_EDITOR
            if (!(Application.isPlaying || this.renderSettings.updateInEditor)) {
                return;
            }
            #endif

            if (this.invalidated) {
                Draw();
            }
        }

        /// <summary>
        /// Draws the texture.
        /// </summary>
        /// <returns>The drawn texture.</returns>
        public Texture2D Draw()
        {
            if (this.texture == null || this.texture.width != this.textureSettings.size.x || this.texture.height != this.textureSettings.size.y) {
                this.texture = new Texture2D(this.textureSettings.size.x, this.textureSettings.size.y);
            }

            this.texture.filterMode = this.textureSettings.filterMode;
            this.texture.wrapMode = this.textureSettings.wrapMode;
            this.invalidated = false;

            SetPixels(this.texture);

            this.texture.Apply();

            ApplyTexture();
            SetTransformScale();

            return this.texture;
        }

        /// <summary>
        /// Sets the pixels of the texture.
        /// </summary>
        /// <param name="texture">The texture to set the pixels on.</param>
        protected abstract void SetPixels(Texture2D texture);

        /// <summary>
        /// Applies the texture to the renderer material.
        /// </summary>
        private void ApplyTexture()
        {
            if (this.renderer == null) {
                return;
            }

            if (Application.isPlaying) {
                this.renderer.material.SetTexture(this.renderSettings.shaderTextureName.id, this.texture);
            } else {
                this.renderer.sharedMaterial = new Material(this.renderer.sharedMaterial);
                this.renderer.sharedMaterial.SetTexture(this.renderSettings.shaderTextureName.id, this.texture);
            }
        }

        /// <summary>
        /// Sets the scale of the transform based on the texture size.
        /// </summary>
        private void SetTransformScale()
        {
            if (this.texture == null || !this.renderSettings.scaleTransform) {
                return;
            }

            Vector3 scale = new Vector2(this.texture.width, this.texture.height);
            scale *= this.renderSettings.scaleFactor;
            scale.z = 1f;

            if (scale.x != Mathf.Infinity && scale.y != Mathf.Infinity) {
                this.transform.localScale = scale;
            }
        }

    }

}
