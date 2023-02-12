using UnityEditor;
using UnityEngine;

namespace Osryden.HexagonFramework.Editor
{
    /// <summary>
    /// Custom property drawer for <see cref="HexagonCoordinates"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(HexagonCoordinates))]
    public class HexagonCoordinatesDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HexagonCoordinates coordinates = (HexagonCoordinates)fieldInfo.GetValue(property.serializedObject.targetObject);

            SerializedProperty q = property.FindPropertyRelative("m_Q");
            SerializedProperty r = property.FindPropertyRelative("m_R");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);

            EditorGUIUtility.labelWidth = 13;

            float width = (position.width - 10) / 3;
            EditorGUI.PropertyField(new Rect(position.x, position.y, width, position.height), q, new GUIContent(nameof(coordinates.Q)));
            EditorGUI.PropertyField(new Rect(position.x + width + 5, position.y, width, position.height), r, new GUIContent(nameof(coordinates.R)));

            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUI.TextField(new Rect(position.x + (width * 2) + 10, position.y, width, position.height), new GUIContent(nameof(coordinates.S)), coordinates.S.ToString());
            }
        }
    }
}
