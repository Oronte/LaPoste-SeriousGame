using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(VisibleAnywhereProperty))]
[CustomPropertyDrawer(typeof(ToggleVisibleAnywhereProperty))]
public class VisibleAnywhereAttributePropertyDrawer : PropertyDrawer
{
    bool value = false;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);

        float _checkBoxWidth = 20.0f;
        float _space = 5.0f;
        ToggleVisibleAnywhereProperty _toggleAttribute = attribute as ToggleVisibleAnywhereProperty;
        //Rect _toggleRect = new Rect(position.x, position.y, _checkBoxWidth, position.height);
        Rect _toggleRect = new Rect(position.x + position.width - _checkBoxWidth, position.y, _checkBoxWidth, position.height);
        value = _toggleAttribute == null ? false : EditorGUI.Toggle(_toggleRect, value);
        GUI.enabled = value;
        if (_toggleAttribute != null) EditorGUIUtility.labelWidth -= _checkBoxWidth;
        float _checkBoxSizeChange = _toggleAttribute == null ? 0 : _toggleRect.width + _space;
        Rect _propRect = new Rect(position.x, position.y, position.width - _checkBoxSizeChange, position.height);
        EditorGUI.PropertyField(_propRect, property, label, true);
        GUI.enabled = true;

    }
}
