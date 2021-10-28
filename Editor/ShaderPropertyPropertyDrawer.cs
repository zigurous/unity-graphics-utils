using UnityEditor;
using UnityEngine;

namespace Zigurous.Graphics.Editor
{
    [CustomPropertyDrawer(typeof(ShaderProperty))]
    public sealed class ShaderPropertyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty name = property.FindPropertyRelative("_name");
            SerializedProperty id = property.FindPropertyRelative("_id");

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            string value = EditorGUI.TextField(position, name.stringValue);

            if (value != name.stringValue)
            {
                name.stringValue = value;
                id.intValue = Shader.PropertyToID(value);
            }

            EditorGUI.EndProperty();
        }

    }

}
