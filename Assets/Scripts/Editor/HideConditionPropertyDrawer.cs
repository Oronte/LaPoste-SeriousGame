using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HideConditionAttribute))]
public class HideConditionPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(ShouldShowProperty(property))
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    private bool ShouldShowProperty(SerializedProperty _property)
    {
        HideConditionAttribute _attribute = attribute as HideConditionAttribute;

        SerializedProperty _serializedProperty =
        _property.serializedObject.FindProperty(_attribute.ConditionSource);

       return _serializedProperty != null && (_serializedProperty.boolValue != _attribute.Invert);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShouldShowProperty(property))
            return -EditorGUIUtility.standardVerticalSpacing;

        return EditorGUI.GetPropertyHeight(property);
    }
}
