using System.IO;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Zigurous.Graphics.Editor
{
    public sealed class TexturePacker : EditorWindow
    {
        private TexturePackerPreset preset;

        private Texture2D red;
        private Texture2D green;
        private Texture2D blue;
        private Texture2D alpha;
        private Texture2D output;

        private float redDefault = 0f;
        private float greenDefault = 1f;
        private float blueDefault = 0f;
        private float alphaDefault = 0.5f;

        private bool redInverted;
        private bool greenInverted;
        private bool blueInverted;
        private bool alphaInverted;

        private Vector2 scrollPosition;

        [MenuItem("Tools/Texture Packer")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(TexturePacker), false, "Texture Packer");
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            EditorGUILayout.Space(10f);
            EditorGUI.BeginChangeCheck();

            preset = (TexturePackerPreset)EditorGUILayout.EnumPopup("Preset", preset);

            if (EditorGUI.EndChangeCheck()) {
                SetDefaults(preset);
            }

            EditorGUILayout.Space(10f);

            switch (preset)
            {
                case TexturePackerPreset.MaskMap:
                    DrawTextureChannel(ref red, ref redDefault, ref redInverted, "Metallic (R)");
                    DrawTextureChannel(ref green, ref greenDefault, ref greenInverted, "Ambient Occlusion (G)");
                    DrawTextureChannel(ref blue, ref blueDefault, ref blueInverted, "Detail Mask (B)");
                    DrawTextureChannel(ref alpha, ref alphaDefault, ref alphaInverted, "Smoothness (A)");
                    break;

                case TexturePackerPreset.Custom:
                    DrawTextureChannel(ref red, ref redDefault, ref redInverted, "Red Channel");
                    DrawTextureChannel(ref green, ref greenDefault, ref greenInverted, "Green Channel");
                    DrawTextureChannel(ref blue, ref blueDefault, ref blueInverted, "Blue Channel");
                    DrawTextureChannel(ref alpha, ref alphaDefault, ref alphaInverted, "Alpha Channel");
                    break;
            }

            DrawOutputSettings(ref output);
            DrawButton("Pack Texture", OnButtonClick);

            EditorGUILayout.EndScrollView();
        }

        private void SetDefaults(TexturePackerPreset preset)
        {
            switch (preset)
            {
                case TexturePackerPreset.MaskMap:
                    redDefault = 0f;
                    greenDefault = 1f;
                    blueDefault = 0f;
                    alphaDefault = 0.5f;
                    break;

                default: break;
            }
        }

        private void DrawTextureChannel(ref Texture2D texture, ref float slider, ref bool inverted, string name)
        {
            EditorGUILayout.LabelField(name, EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

            texture = (Texture2D)EditorGUILayout.ObjectField("Map", texture, typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            if (texture == null) {
                slider = EditorGUILayout.Slider("Default Value", slider, 0f, 1f);
            }

            inverted = EditorGUILayout.Toggle("Inverted", inverted);

            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10f);
        }

        private void DrawOutputSettings(ref Texture2D ouput)
        {
            EditorGUILayout.LabelField("Output (RGBA)", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

            ouput = (Texture2D)EditorGUILayout.ObjectField("Mask", ouput, typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10f);
        }

        private void DrawButton(string label, System.Action onClick)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(label, GUILayout.Width(200f), GUILayout.Height(25f))) {
                onClick();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void OnButtonClick()
        {
            string assetPath = AssetDatabase.GetAssetPath(output);
            string fullPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.dataPath), assetPath));
            string extension = Path.GetExtension(fullPath);
            Texture2D packed = PackChannels(output.width, output.height);
            File.WriteAllBytes(fullPath, Encode(packed, extension));
            EditorUtility.CopySerialized(packed, output);
            EditorUtility.SetDirty(output);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private Texture2D PackChannels(int width, int height)
        {
            int pixelCount = width * height;
            Texture2D packed = new(width, height, TextureFormat.RGBA32, false);
            Color32[] pixels = new Color32[pixelCount];

            bool hasRed = red != null;
            bool hasGreen = green != null;
            bool hasBlue = blue != null;
            bool hasAlpha = alpha != null;

            byte redDefault = (byte)Mathf.Round(this.redDefault * 255f);
            byte greenDefault = (byte)Mathf.Round(this.greenDefault * 255f);
            byte blueDefault = (byte)Mathf.Round(this.blueDefault * 255f);
            byte alphaDefault = (byte)Mathf.Round(this.alphaDefault * 255f);

            Color32[] rData = hasRed ? red.GetPixels32() : null;
            Color32[] gData = hasGreen ? green.GetPixels32() : null;
            Color32[] bData = hasBlue ? blue.GetPixels32() : null;
            Color32[] aData = hasAlpha ? alpha.GetPixels32() : null;

            for (int i = 0; i < pixelCount; i++)
            {
                byte r = hasRed ? rData[i].r : redDefault;
                byte g = hasGreen ? gData[i].r : greenDefault;
                byte b = hasBlue ? bData[i].r : blueDefault;
                byte a = hasAlpha ? aData[i].r : alphaDefault;

                Color color = new Color32(r, g, b, a);

                if (redInverted) color.r = 1f - color.r;
                if (greenInverted) color.g = 1f - color.g;
                if (blueInverted) color.b = 1f - color.b;
                if (alphaInverted) color.a = 1f - color.a;

                pixels[i] = color;
            }

            packed.SetPixels32(pixels);
            packed.Apply();

            return packed;
        }

        private static byte[] Encode(Texture2D texture, string fileExtension)
        {
            return fileExtension.ToLowerInvariant() switch
            {
                ".exr" => texture.EncodeToEXR(),
                ".jpg" => texture.EncodeToJPG(),
                ".jpeg" => texture.EncodeToJPG(),
                ".png" => texture.EncodeToPNG(),
                ".tga" => texture.EncodeToTGA(),
                _ => texture.EncodeToPNG(),
            };
        }

    }

    public enum TexturePackerPreset
    {
        MaskMap,
        Custom,
    }

}
