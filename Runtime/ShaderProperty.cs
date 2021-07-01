using UnityEngine;

namespace Zigurous.Graphics
{
    /// <summary>
    /// A shader property that can be set on a material. An id is automatically
    /// created for the property for optimal code.
    /// </summary>
    [System.Serializable]
    public struct ShaderProperty
    {
        [SerializeField]
        [HideInInspector]
        private int _id;

        /// <summary>
        /// The id of the shader property.
        /// </summary>
        public int id
        {
            get
            {
                if (_id == 0) {
                    _id = Shader.PropertyToID(_name);
                }
                return _id;
            }
        }

        [SerializeField]
        [Tooltip("The name of the shader property.")]
        private string _name;

        /// <summary>
        /// The name of the shader property.
        /// </summary>
        public string name
        {
            get => _name;
            set
            {
                _name = value;
                _id = Shader.PropertyToID(value);
            }
        }

        /// <summary>
        /// Constructs a new shader property with the given name.
        /// </summary>
        public ShaderProperty(string name)
        {
            _name = name;
            _id = Shader.PropertyToID(name);
        }

        public static implicit operator ShaderProperty(string name) => new ShaderProperty(name);

    }

}
