using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSlider : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] Slider slider = null;
    [SerializeField] TextMeshProUGUI text = null;

    private void Start()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        if (!slider) return;
        if (text) text.text = $"{(int)(slider.value / slider.maxValue * 100)}%";
    }
}
