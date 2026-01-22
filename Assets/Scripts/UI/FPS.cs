using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] TextMeshProUGUI text = null;

    // Update is called once per frame
    void Update()
    {
        UpdateFPS();
    }

    void UpdateFPS()
    {
        if (!text) return;
        float _fps = 1.0f / Time.deltaTime;
        text.text = $"{_fps.ToString("0.0")} FPS ({(1000.0f / _fps).ToString("0.00")}ms)";
    }
}
