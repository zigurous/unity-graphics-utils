﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
#endif
using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Automatically tiles the material textures based on the object's scale.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu("Zigurous/Graphics/Auto Tile")]
    public sealed class AutoTile : MonoBehaviour
    {
        /// <summary>
        /// An axis along which a texture is tiled.
        /// </summary>
        public enum Axis
        {
            [InspectorName("X+")] X_Pos,
            [InspectorName("X-")] X_Neg,
            [InspectorName("Y+")] Y_Pos,
            [InspectorName("Y-")] Y_Neg,
            [InspectorName("Z+")] Z_Pos,
            [InspectorName("Z-")] Z_Neg,
        }

        /// <summary>
        /// A representation of a submesh that can be individually tiled.
        /// </summary>
        [System.Serializable]
        public sealed class Submesh
        {
            #if UNITY_EDITOR
            /// <summary>
            /// A copy of the shared material used for changes in the editor.
            /// </summary>
            internal Material editorMaterial;
            #endif

            /// <summary>
            /// The axis along which the texture is tiled.
            /// </summary>
            [Tooltip("The axis along which the texture is tiled.")]
            public Axis axis = Axis.Y_Pos;

            /// <summary>
            /// The submesh index of the material being tiled.
            /// </summary>
            [Tooltip("The submesh index of the material being tiled.")]
            public int submeshIndex = 0;

            /// <summary>
            /// The object's base unit scale. For example, planes have a unit
            /// scale of 10 compared to most other primitives.
            /// </summary>
            [Tooltip("The object's base unit scale. For example, planes have a unit scale of 10 compared to most other primitives.")]
            public Vector3 unitScale = Vector3.one;

            /// <summary>
            /// The texture offset applied on the material.
            /// </summary>
            [Tooltip("The texture offset applied on the material.")]
            public Vector2 textureOffset = Vector2.zero;
        }

        /// <summary>
        /// The renderer component of the material being tiled.
        /// </summary>
        public new Renderer renderer { get; private set; }

        /// <summary>
        /// The submeshes that are tiled on the renderer.
        /// </summary>
        [Tooltip("The submeshes that are tiled on the renderer.")]
        public Submesh[] submeshes = new Submesh[1] { new Submesh() };

        /// <summary>
        /// The names of the textures that are tiled on the material.
        /// </summary>
        [Tooltip("The names of the textures that are tiled on the material.")]
        public string[] textureNames = new string[] { "_MainTex" };

        /// <summary>
        /// Whether the material texture(s) are tiled automatically when the
        /// transform changes.
        /// </summary>
        [Tooltip("Whether the material texture(s) are tiled automatically when the transform changes.")]
        public bool autoUpdate = true;

        #if UNITY_EDITOR
        /// <summary>
        /// Whether the material texture(s) are tiled while in the editor.
        /// </summary>
        [Tooltip("Whether the material texture(s) are tiled while in the editor.")]
        public bool updateInEditor = true;
        #endif

        private void Reset()
        {
            MeshFilter filter = GetComponent<MeshFilter>();

            if (filter != null)
            {
                Mesh mesh = Application.isPlaying ? filter.mesh : filter.sharedMesh;

                if (mesh != null)
                {
                    this.submeshes = new Submesh[mesh.subMeshCount];

                    for (int i = 0; i < this.submeshes.Length; i++)
                    {
                        Submesh submesh = new Submesh();
                        submesh.submeshIndex = i;
                        this.submeshes[i] = submesh;
                    }
                }
            }
        }

        private void OnValidate()
        {
            if (this.enabled) {
                Tile();
            }
        }

        private void LateUpdate()
        {
            if (this.autoUpdate && this.transform.hasChanged)
            {
                Tile();
                this.transform.hasChanged = false;
            }
        }

        public void Tile()
        {
            #if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(this) || PrefabStageUtility.GetCurrentPrefabStage() != null) {
                return;
            }
            #endif

            if (this.renderer == null) {
                this.renderer = GetComponent<Renderer>();
            }

            if (Application.isPlaying)
            {
                Material[] materials = this.renderer.materials;

                if (materials != null) {
                    UpdateMaterials(materials);
                }
            }
            #if UNITY_EDITOR
            else if (this.updateInEditor)
            {
                Material[] materials = this.renderer.sharedMaterials;

                if (materials != null) {
                    UpdateMaterialsInEditor(materials);
                }
            }
            #endif
        }

        private void UpdateMaterials(Material[] materials)
        {
            for (int i = 0; i < this.submeshes.Length; i++)
            {
                Submesh submesh = this.submeshes[i];

                if (submesh.submeshIndex >= 0 && submesh.submeshIndex < materials.Length) {
                    UpdateMaterial(materials[submesh.submeshIndex], submesh);
                }
            }
        }

        #if UNITY_EDITOR
        private void UpdateMaterialsInEditor(Material[] materials)
        {
            for (int i = 0; i < this.submeshes.Length; i++)
            {
                Submesh submesh = this.submeshes[i];

                if (submesh.submeshIndex >= 0 && submesh.submeshIndex < materials.Length)
                {
                    Material sharedMaterial = materials[submesh.submeshIndex];

                    if (sharedMaterial != null)
                    {
                        // Save a copy of the material so we do not have to
                        // create a new one every time
                        if (sharedMaterial != submesh.editorMaterial)
                        {
                            submesh.editorMaterial = new Material(sharedMaterial);
                            materials[submesh.submeshIndex] = submesh.editorMaterial;
                            this.renderer.sharedMaterials = materials;
                        }

                        UpdateMaterial(submesh.editorMaterial, submesh);
                    }
                }
            }
        }
        #endif

        private void UpdateMaterial(Material material, Submesh submesh)
        {
            if (material == null || this.textureNames == null) {
                return;
            }

            Vector2 textureScale = GetTextureScale(submesh.axis, submesh.unitScale);
            Vector2 textureOffset = submesh.textureOffset;

            for (int i = 0; i < this.textureNames.Length; i++)
            {
                string property = this.textureNames[i];

                material.SetTextureScale(property, textureScale);
                material.SetTextureOffset(property, textureOffset);
            }
        }

        private Vector2 GetTextureScale(Axis axis, Vector3 baseScale)
        {
            Vector3 lossy = this.transform.lossyScale;
            Vector3 scale = Vector3.Scale(lossy, baseScale);

            switch (axis)
            {
                case Axis.X_Pos:
                    return new Vector2(scale.z, scale.y);

                case Axis.X_Neg:
                    return -new Vector2(scale.z, scale.y);

                case Axis.Y_Pos:
                    return new Vector2(scale.x, scale.z);

                case Axis.Y_Neg:
                    return -new Vector2(scale.x, scale.z);

                case Axis.Z_Pos:
                    return new Vector2(scale.x, scale.y);

                case Axis.Z_Neg:
                    return -new Vector2(scale.x, scale.y);

                default:
                    return Vector2.zero;
            }
        }

    }

}