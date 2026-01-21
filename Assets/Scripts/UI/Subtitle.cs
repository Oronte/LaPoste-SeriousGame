using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Subtitle : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] TextMeshProUGUI surname = null;
    [SerializeField] Image icon = null;
    [SerializeField] TextMeshProUGUI dialog = null;
    [SerializeField] Image panel = null;

    [Header("Debug", order = 10)]
    [SerializeField] string nameText = "";
    [SerializeField] Sprite iconImage = null;
    [SerializeField] string dialogText = "";
    [SerializeField] float duration = 3.0f;

    bool isVisible = false;
    float timer = 0.0f;

    void Start()
    {
        SetVisibility(isVisible);
        SetDialog(nameText, iconImage, dialogText); // Debug
    }

    private void Update()
    {
        if (!isVisible) return;
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0.0f;
            SetVisibility(false);
        }
    }

    public void SetDialog(string _name, Sprite _icon, string _dialog)
    {
        if (surname) surname.text = _name;
        if (icon) icon.sprite = _icon;
        if (dialog) dialog.text = _dialog;

        timer = 0.0f;
        SetVisibility(true);
    }

    void SetVisibility(bool _visible)
    {
        if (panel) panel.gameObject.SetActive(_visible);
        if (surname) surname.gameObject.SetActive(_visible);
        if (icon) icon.gameObject.SetActive(_visible);
        if (dialog) dialog.gameObject.SetActive(_visible);
        isVisible = _visible;
    }
}
