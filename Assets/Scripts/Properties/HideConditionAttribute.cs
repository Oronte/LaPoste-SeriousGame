using UnityEngine;

public sealed class HideConditionAttribute : PropertyAttribute
{
    public string ConditionSource { get; private set; } = "";
    public bool Invert { get; private set; } = false;

    public HideConditionAttribute(string _conditionSource, bool _invert = false)
    {
        ConditionSource = _conditionSource;
        Invert = _invert;
    }
}
