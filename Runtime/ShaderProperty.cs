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
        /// The id of the shader property (Read only).
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
        /// Creates a new shader property with the given name.
        /// </summary>
        /// <param name="name">The name of the shader property.</param>
        public ShaderProperty(string name)
        {
            _name = name;
            _id = Shader.PropertyToID(name);
        }

        /// <summary>
        /// Implicitly converts a name to a shader property.
        /// </summary>
        /// <param name="name">The name of the shader property.</param>
        /// <returns>A shader property with the given name.</returns>
        public static implicit operator ShaderProperty(string name) => new ShaderProperty(name);

        /// <summary>
        /// Implicitly converts a shader property to an id.
        /// </summary>
        /// <param name="property">The shader property to convert to an id.</param>
        /// <returns>The id of the shader property.</returns>
        public static implicit operator int(ShaderProperty property) => property.id;

    }

}
