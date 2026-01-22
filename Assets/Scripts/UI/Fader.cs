using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    enum FadeState
    {
        None,
        FadeIn,
        FadeOut,
        FadeInOut_In,
        FadeInOut_Out,
    }

    [Header("References", order = 0)]
    [SerializeField] Image image = null;

    [Header("Config", order = 1)]
    [SerializeField] Color fadeColor = Color.black;
    [SerializeField] float speedMultiplier = 1.0f;

    [Header("Events", order = 2)]
    [SerializeField] UnityEvent onFadeFinished = null;
    [SerializeField] UnityEvent onBetweenFadeInOut = null;

    FadeState state = FadeState.None;
    float fade = 0.0f;

    void Start()
    {
        if (!image) return;
        image.color = fadeColor;
    }

    void Update()
    {
        if (!image || state == FadeState.None) return;

        switch (state)
        {
            case FadeState.FadeIn:
                FadeTo(1.0f, FadeState.None);
                break;

            case FadeState.FadeOut:
                FadeTo(0.0f, FadeState.None);
                break;

            case FadeState.FadeInOut_In:
                FadeTo(1.0f, FadeState.FadeInOut_Out);
                break;

            case FadeState.FadeInOut_Out:
                FadeTo(0.0f, FadeState.None);
                break;
        }
    }

    void FadeTo(float _target, FadeState _next)
    {
        fade = Mathf.MoveTowards(fade, _target, Time.deltaTime * speedMultiplier);
        SetAlpha(fade);
        if (Mathf.Approximately(fade, _target))
        {
            if (_next != FadeState.FadeInOut_Out)
                onFadeFinished?.Invoke();
            else
                onBetweenFadeInOut?.Invoke();

            state = _next;
        }
    }

    void SetAlpha(float _aplha)
    {
        if (!image) return;

        Color _color = image.color;
        _color.a = _aplha;
        image.color = _color;
    }

    public void FadeIn()
    {
        state = FadeState.FadeIn;
    }

    public void FadeInOut()
    {
        state = FadeState.FadeInOut_In;
    }

    public void FadeOut()
    {
        state = FadeState.FadeOut;
    }
}
