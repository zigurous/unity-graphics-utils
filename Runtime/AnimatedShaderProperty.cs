using System.Collections.Generic;
using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// A shader property that can be animated.
    /// </summary>
    [System.Serializable]
    public abstract class AnimatedShaderProperty
    {
        /// <summary>
        /// The shader property to animate.
        /// </summary>
        [Tooltip("The shader property to animate.")]
        public ShaderProperty property;

        /// <summary>
        /// Creates a new animated shader property.
        /// </summary>
        /// <param name="property">The shader property to animate.</param>
        public AnimatedShaderProperty(string property)
        {
            this.property = property;
        }

        /// <summary>
        /// Animates the shader property.
        /// </summary>
        /// <param name="material">The material to animate.</param>
        /// <param name="time">The time of the animation to evaluate.</param>
        public abstract void Animate(Material material, float time);

    }

    /// <summary>
    /// A shader float property that can be animated.
    /// </summary>
    [System.Serializable]
    public class AnimatedShaderFloatProperty : AnimatedShaderProperty
    {
        /// <summary>
        /// The value over time of the shader property.
        /// </summary>
        [Tooltip("The value over time of the shader property.")]
        public AnimationCurve valueOverTime;

        /// <summary>
        /// Creates a new animated shader float property.
        /// </summary>
        /// <param name="valueOverTime">The value over time of the shader property.</param>
        /// <param name="property">The shader property to animate.</param>
        public AnimatedShaderFloatProperty(AnimationCurve valueOverTime, string property) : base(property)
        {
            this.valueOverTime = valueOverTime;
        }

        /// <inheritdoc/>
        public override void Animate(Material material, float time)
        {
            material.SetFloat(this.property.id, this.valueOverTime.Evaluate(time));
        }

    }

    /// <summary>
    /// A shader int property that can be animated.
    /// </summary>
    [System.Serializable]
    public class AnimatedShaderIntProperty : AnimatedShaderProperty
    {
        /// <summary>
        /// The value over time of the shader property.
        /// </summary>
        [Tooltip("The value over time of the shader property.")]
        public AnimationCurve valueOverTime;

        /// <summary>
        /// Creates a new animated shader int property.
        /// </summary>
        /// <param name="valueOverTime">The value over time of the shader property.</param>
        /// <param name="property">The shader property to animate.</param>
        public AnimatedShaderIntProperty(AnimationCurve valueOverTime, string property) : base(property)
        {
            this.valueOverTime = valueOverTime;
        }

        /// <inheritdoc/>
        public override void Animate(Material material, float time)
        {
            material.SetInt(this.property.id, (int)this.valueOverTime.Evaluate(time));
        }

    }

    /// <summary>
    /// A shader color property that can be animated.
    /// </summary>
    [System.Serializable]
    public class AnimatedShaderColorProperty : AnimatedShaderProperty
    {
        /// <summary>
        /// The color over time of the shader property.
        /// </summary>
        public Gradient colorOverTime;

        /// <summary>
        /// Creates a new animated shader color property.
        /// </summary>
        /// <param name="colorOverTime">The color over time of the shader property.</param>
        /// <param name="property">The shader property to animate.</param>
        public AnimatedShaderColorProperty(Gradient colorOverTime, string property) : base(property)
        {
            this.colorOverTime = colorOverTime;
        }

        /// <inheritdoc/>
        public override void Animate(Material material, float time)
        {
            material.SetColor(this.property.id, this.colorOverTime.Evaluate(time));
        }

    }

    /// <summary>
    /// Extension methods for <see cref="AnimatedShaderProperty"/>.
    /// </summary>
    public static class AnimatedShaderPropertyExtensions
    {
        /// <summary>
        /// Animates an array of shader properties.
        /// </summary>
        /// <param name="properties">The shader properties to animate.</param>
        /// <param name="material">The material to animate.</param>
        /// <param name="time">The time of the animation to evaluate.</param>
        public static void Animate(this AnimatedShaderProperty[] properties, Material material, float time)
        {
            for (int i = 0; i < properties.Length; i++) {
                properties[i].Animate(material, time);
            }
        }

        /// <summary>
        /// Animates an array of shader properties.
        /// </summary>
        /// <param name="properties">The shader properties to animate.</param>
        /// <param name="material">The material to animate.</param>
        /// <param name="time">The time of the animation to evaluate.</param>
        public static void Animate(this List<AnimatedShaderProperty> properties, Material material, float time)
        {
            foreach (AnimatedShaderProperty property in properties) {
                property.Animate(material, time);
            }
        }

    }

}
