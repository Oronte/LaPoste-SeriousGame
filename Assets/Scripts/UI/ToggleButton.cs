using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] Toggle toggle = null;

    [Header("Events", order = 1)]
    [SerializeField] UnityEvent onOn = null;
    [SerializeField] UnityEvent onOff = null;

    private void Start()
    {
        if (!toggle) return;
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(bool _value)
    {
        if (_value) onOn?.Invoke();
        else onOff?.Invoke();
    }
}
