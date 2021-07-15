using UnityEngine;
using UnityEngine.Rendering;

namespace Zigurous.Graphics
{
    /// <summary>
    /// Extension methods for materials.
    /// </summary>
    public static class MaterialExtensions
    {
        /// <summary>
        /// Returns the rendering mode of the material, see <see cref="RenderingMode"/>.
        /// </summary>
        /// <param name="material">The material to get the rendering mode from.</param>
        public static RenderingMode GetRenderingMode(this Material material)
        {
            int mode = (int)material.GetFloat(Identifier.Mode);

            switch (mode)
            {
                case 1:
                    return RenderingMode.Cutout;
                case 2:
                    return RenderingMode.Fade;
                case 3:
                    return RenderingMode.Transparent;
                default:
                    return RenderingMode.Opaque;
            }
        }

        /// <summary>
        /// Sets the rendering mode of the material, see <see cref="RenderingMode"/>.
        /// </summary>
        /// <param name="material">The material to set the rendering mode on.</param>
        /// <param name="mode">The rendering mode to set.</param>
        public static void SetRenderingMode(this Material material, RenderingMode mode)
        {
            switch (mode)
            {
                case RenderingMode.Opaque:
                    material.SetOverrideTag("RenderType", "");
                    material.SetFloat(Identifier.Mode, 0);
                    material.SetInt(Identifier.SrcBlend, (int)BlendMode.One);
                    material.SetInt(Identifier.DstBlend, (int)BlendMode.Zero);
                    material.SetInt(Identifier.ZWrite, 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;

                case RenderingMode.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetFloat(Identifier.Mode, 1);
                    material.SetInt(Identifier.SrcBlend, (int)BlendMode.One);
                    material.SetInt(Identifier.DstBlend, (int)BlendMode.Zero);
                    material.SetInt(Identifier.ZWrite, 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 2450;
                    break;

                case RenderingMode.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetFloat(Identifier.Mode, 2);
                    material.SetInt(Identifier.SrcBlend, (int)BlendMode.SrcAlpha);
                    material.SetInt(Identifier.DstBlend, (int)BlendMode.OneMinusSrcAlpha);
                    material.SetInt(Identifier.ZWrite, 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;

                case RenderingMode.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetFloat(Identifier.Mode, 3);
                    material.SetInt(Identifier.SrcBlend, (int)BlendMode.One);
                    material.SetInt(Identifier.DstBlend, (int)BlendMode.OneMinusSrcAlpha);
                    material.SetInt(Identifier.ZWrite, 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;
            }
        }

    }

}
