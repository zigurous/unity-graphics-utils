using UnityEngine;

namespace Zigurous.Graphics
{
    public static class Identifier
    {
        /// <summary>Shader.PropertyToID("_Color")</summary>
        public static readonly int Color = Shader.PropertyToID("_Color");

        /// <summary>Shader.PropertyToID("_Cubemap")</summary>
        public static readonly int Cubemap = Shader.PropertyToID("_Cubemap");

        /// <summary>Shader.PropertyToID("_DstBlend")</summary>
        public static readonly int DstBlend = Shader.PropertyToID("_DstBlend");

        /// <summary>Shader.PropertyToID("_Exposure")</summary>
        public static readonly int Exposure = Shader.PropertyToID("_Exposure");

        /// <summary>Shader.PropertyToID("_Glossiness")</summary>
        public static readonly int Glossiness = Shader.PropertyToID("_Glossiness");

        /// <summary>Shader.PropertyToID("_MainTex")</summary>
        public static readonly int MainTex = Shader.PropertyToID("_MainTex");

        /// <summary>Shader.PropertyToID("_Metallic")</summary>
        public static readonly int Metallic = Shader.PropertyToID("_Metallic");

        /// <summary>Shader.PropertyToID("_Mode")</summary>
        public static readonly int Mode = Shader.PropertyToID("_Mode");

        /// <summary>Shader.PropertyToID("_Ramp")</summary>
        public static readonly int Ramp = Shader.PropertyToID("_Ramp");

        /// <summary>Shader.PropertyToID("_SrcBlend")</summary>
        public static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");

        /// <summary>Shader.PropertyToID("_ZWrite")</summary>
        public static readonly int ZWrite = Shader.PropertyToID("_ZWrite");
    }

}
