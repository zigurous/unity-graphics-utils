using UnityEditor;
using UnityEngine;

namespace Zigurous.Graphics.Editor
{
    [CustomPropertyDrawer(typeof(ShaderProperty))]
    public sealed class ShaderPropertyPropertyDrawer : PropertyDrawer
    {
        private SerializedProperty _name;
        private SerializedProperty _id;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_name == null) {
                _name = property.FindPropertyRelative("_name");
            }

            if (_id == null) {
                _id = property.FindPropertyRelative("_id");
            }

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            string name = EditorGUI.TextField(position, _name.stringValue);

            if (name != _name.stringValue)
            {
                _name.stringValue = name;
                _id.intValue = Shader.PropertyToID(name);
            }

            EditorGUI.EndProperty();
        }

    }

}
