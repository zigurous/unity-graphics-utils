namespace Zigurous.Graphics
{
    /// <summary>
    /// A rendering blend mode of a material.
    /// </summary>
    public enum RenderingMode
    {
        /// <summary>
        /// The default rendering mode, and suitable for normal solid objects
        /// with no transparent areas.
        /// </summary>
        Opaque,

        /// <summary>
        /// Allows you to create a transparent effect that has hard edges
        /// between the opaque and transparent areas. In this mode, there are no
        /// semi-transparent areas, the texture is either 100% opaque, or
        /// invisible. This is useful when using transparency to create the
        /// shape of materials such as leaves, or cloth with holes and tatters.
        /// </summary>
        Cutout,

        /// <summary>
        /// Allows the transparency values to entirely fade an object out,
        /// including any specular highlights or reflections it may have. This
        /// mode is useful if you want to animate an object fading in or out. It
        /// is not suitable for rendering realistic transparent materials such
        /// as clear plastic or glass because the reflections and highlights
        /// will also be faded out.
        /// </summary>
        Fade,

        /// <summary>
        /// Suitable for rendering realistic transparent materials such as clear
        /// plastic or glass. In this mode, the material itself will take on
        /// transparency values (based on the texture’s alpha channel and the
        /// alpha of the tint colour), however reflections and lighting
        /// highlights will remain visible at full clarity as is the case with
        /// real transparent materials.
        /// </summary>
        Transparent,
    }

}
