using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DisplayTextAttribute))]
public class DisplayTextDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DisplayTextAttribute _displayAttr = (DisplayTextAttribute)attribute;
        label = EditorGUI.BeginProperty(position, label, property);
        label.text = _displayAttr.Text;
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
