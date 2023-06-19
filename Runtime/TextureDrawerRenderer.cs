using UnityEditor;
using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Renders the result of a <see cref="TextureDrawer"/>.
    /// </summary>
    [ExecuteAlways]
    [AddComponentMenu("Zigurous/Graphics/Texture Drawer Renderer")]
    [HelpURL("https://docs.zigurous.com/com.zigurous.graphics/api/Zigurous.Graphics/TextureDrawerRenderer")]
    [RequireComponent(typeof(Renderer))]
    public sealed class TextureDrawerRenderer : MonoBehaviour
    {
        private Renderer m_Renderer;

        [Tooltip("The drawer that creates the texture.")]
        [SerializeField] private TextureDrawer m_Drawer;

        [Tooltip("The shader property that holds the texture.")]
        [SerializeField] private string m_ShaderTextureName = "_MainTex";

        [Tooltip("The amount of scaling to apply to the transform (as a multiplier).")]
        [SerializeField] private float m_ScaleFactor = 1f;

        [Tooltip("Scales the transform of the object to match the texture size.")]
        [SerializeField] private bool m_ScaleTransform = false;

        /// <summary>
        /// The drawer that creates the texture.
        /// </summary>
        public TextureDrawer drawer
        {
            get => m_Drawer;
            set
            {
                m_Drawer = value;
                Render();
            }
        }

        /// <summary>
        /// The shader property that holds the texture.
        /// </summary>
        public string shaderTextureName
        {
            get => m_ShaderTextureName;
            set
            {
                m_ShaderTextureName = value;
                Render();
            }
        }

        /// <summary>
        /// The amount of scaling to apply to the transform (as a multiplier).
        /// </summary>
        public float scaleFactor
        {
            get => m_ScaleFactor;
            set
            {
                m_ScaleFactor = value;
                Render();
            }
        }

        /// <summary>
        /// Scales the transform of the object to match the texture size.
        /// </summary>
        public bool scaleTransform
        {
            get => m_ScaleTransform;
            set
            {
                m_ScaleTransform = value;
                Render();
            }
        }

        #if UNITY_EDITOR
        [SerializeField]
        private bool m_UpdateInEditor;
        private bool m_Invalidated;

        private void OnValidate()
        {
            if (m_UpdateInEditor) {
                m_Invalidated = true;
            }
        }

        private void Update()
        {
            if (m_Invalidated)
            {
                ForceRender();
                m_Invalidated = false;
            }
        }
        #endif

        private void OnEnable()
        {
            Render();
        }

        private void Render()
        {
            if (enabled && Application.isPlaying) {
                ForceRender();
            }
        }

        private void ForceRender()
        {
            if (drawer == null) {
                return;
            }

            if (m_Renderer == null) {
                m_Renderer = GetComponent<Renderer>();
            }

            Texture2D texture = drawer.Draw();

            if (Application.isPlaying) {
                m_Renderer.material.SetTexture(shaderTextureName, texture);
            } else {
                m_Renderer.sharedMaterial = new Material(m_Renderer.sharedMaterial);
                m_Renderer.sharedMaterial.SetTexture(shaderTextureName, texture);
            }

            if (scaleTransform)
            {
                Vector3 scale = new Vector2(texture.width, texture.height) * scaleFactor;
                scale.z = 1f;

                if (scale.x != Mathf.Infinity && scale.y != Mathf.Infinity) {
                    transform.localScale = scale;
                }
            }
        }

        #if UNITY_EDITOR
        [MenuItem("CONTEXT/TextureDrawerRenderer/Force Update")]
        private static void ForceUpdate()
        {
            if (Selection.activeGameObject != null)
            {
                TextureDrawerRenderer renderer = Selection.activeGameObject.GetComponent<TextureDrawerRenderer>();

                if (renderer != null)
                {
                    if (renderer.m_UpdateInEditor) {
                        renderer.m_Invalidated = true;
                    } else if (Application.isPlaying) {
                        renderer.ForceRender();
                    }
                }
            }
        }
        #endif

    }

}
