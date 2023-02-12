using UnityEditor;
using UnityEngine;

namespace Osryden.HexagonFramework.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="HexagonGeometry"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(HexagonGeometry))]
    public class HexagonGeometryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty orientation = property.FindPropertyRelative("m_Orientation");
            SerializedProperty size = property.FindPropertyRelative("m_Size");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);

            EditorGUI.PropertyField(new Rect(position.x, position.y, 84, position.height), orientation, GUIContent.none);
            EditorGUIUtility.labelWidth = 28;
            EditorGUI.PropertyField(new Rect(position.x + 84 + 5, position.y, position.width - 84 - 5, position.height), size);
        }
    }
}
