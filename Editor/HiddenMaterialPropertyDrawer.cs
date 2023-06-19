using UnityEditor;
using UnityEngine;

namespace Zigurous.Graphics.Editor
{
    public sealed class HiddenMaterialPropertyDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor) {}

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }

    }

}
