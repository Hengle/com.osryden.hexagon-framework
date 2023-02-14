using UnityEditor;
using UnityEngine;

namespace Osryden.HexagonFramework.Editor
{
    [CustomPropertyDrawer(typeof(HexagonCoordinates))]
    public class HexagonCoordinatesDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty q = property.FindPropertyRelative("m_Q");
            SerializedProperty r = property.FindPropertyRelative("m_R");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUIUtility.labelWidth = 13;

            float width = (position.width - 10) / 3;
            EditorGUI.PropertyField(new Rect(position.x, position.y, width, position.height), q, new GUIContent("Q"));
            EditorGUI.PropertyField(new Rect(position.x + width + 5, position.y, width, position.height), r, new GUIContent("R"));

            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUI.IntField(new Rect(position.x + (width * 2) + 10, position.y, width, position.height), new GUIContent("S"), -q.intValue + r.intValue);
            }

            EditorGUI.indentLevel = indent;
        }
    }
}
