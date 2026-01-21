using System;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class RadialProgressBar : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] Image backgroundImage = null;
    [SerializeField] Image fillImage = null;
    [SerializeField] TextMeshProUGUI percentText = null;

    [Header("Compteur", order = 1)]
    [SerializeField] float time = 1.0f;
    float defaultTime = 1.0f;
    [SerializeField] float maxTime = 1.0f;
    [SerializeField] float incrementMultiplier = 1.0f;
    [SerializeField] float decrementMultiplier = 1.0f;
    [SerializeField] bool hideIfFinish = false;
    [SerializeField] bool resetIfFinish = false;

    [Header("Color", order = 2)]
    [SerializeField] Color startColor = Color.green;
    [SerializeField] Color endColor = Color.red;
    [SerializeField] Color backgroundColor = new Color(22, 22, 22);
    [SerializeField] Color textColor = Color.white;

    [Header("Event", order = 3)]
    [SerializeField] UnityEvent onMinValue = null;
    [SerializeField] UnityEvent onMaxValue = null;
    public UnityEvent OnMinValue => onMinValue;
    public UnityEvent OnMaxValue => onMaxValue;

    [SerializeField, InspectorName("Activer ?")] bool activate = false;
    public bool Activate { get => activate; set => activate = value; }

    public void Init()
    {
        defaultTime = time;
        if (percentText) percentText.color = textColor;
        if (backgroundImage) backgroundImage.color = backgroundColor;
        if (fillImage) fillImage.color = startColor;
        if (fillImage) fillImage.fillAmount = time;
        UpdateText();
    }

    public void Reset()
    {
        time = defaultTime;
    }

    private void Start()
    {
        Init();//TODO Remove, the storage letter game will do it
    }

    void Update()
    {
        if (!fillImage) return;

        if (activate) IncrementProgressBar();

        if (!activate && time >= maxTime)
            DecrementProgressBar();
        else if (!activate && time > 0.0f && time < maxTime)
            DecrementProgressBar();
    }

    void IncrementProgressBar()
    {
        SetVisibility(true);
        time += Time.deltaTime * incrementMultiplier;
        fillImage.fillAmount = time/maxTime;

        if (time >= maxTime)
        {
            onMaxValue?.Invoke();
            //activate = false;
            time = resetIfFinish ? 0.0f : maxTime;
            fillImage.fillAmount = resetIfFinish ? 0.0f : 1.0f;
            if (hideIfFinish) SetVisibility(false);
        }

        UpdateText();
        UpdateColor();
    }

    void DecrementProgressBar()
    {
        SetVisibility(true);
        time -= Time.deltaTime * decrementMultiplier;
        fillImage.fillAmount = time/maxTime;

        if (time <= 0.0f)
        {
            onMinValue?.Invoke();
            activate = false;
            time = 0.0f;
            fillImage.fillAmount = 0.0f;
            if (hideIfFinish) SetVisibility(false);
        }

        UpdateText();
        UpdateColor();
    }

    void UpdateText()
    {
        if (!percentText) return;

        percentText.text = $"{(int)(time / maxTime * 100)}%";
    }

    void SetVisibility(bool _visible)
    {
        if (backgroundImage) backgroundImage.gameObject.SetActive(_visible);
        if (fillImage) fillImage.gameObject.SetActive(_visible);
        if (percentText) percentText.gameObject.SetActive(_visible);
    }

    void UpdateColor()
    {
        if (fillImage) fillImage.color = Color.Lerp(startColor, endColor, time / maxTime);
    }
}
