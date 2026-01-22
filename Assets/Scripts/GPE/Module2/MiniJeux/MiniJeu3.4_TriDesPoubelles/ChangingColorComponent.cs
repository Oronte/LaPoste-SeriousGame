using UnityEngine;

public class ChangingColorComponent : MonoBehaviour
{
    [SerializeField, VisibleAnywhereProperty] MeshRenderer renderer = null;
    [SerializeField, VisibleAnywhereProperty] float currentTime = 0f;
    [SerializeField] float maxTime = 10f;
    [SerializeField, VisibleAnywhereProperty] Color startColor;
    [SerializeField] Color targetColor = Color.gray;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        startColor = renderer.material.color;
    }

    private void Update()
    {
        ChangeColor();
        UpdateTime();
    }

    void ChangeColor()
    {
        renderer.material.color = Color.Lerp(startColor, targetColor, currentTime / maxTime);
    }

    void UpdateTime()
    {
        currentTime += Time.deltaTime;
        if (currentTime > maxTime)
        {
            currentTime = 0f;
            Color _temp = startColor;
            startColor = targetColor;
            targetColor = _temp;
        }
    }
}
