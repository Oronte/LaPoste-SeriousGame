using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SaturateCameraEffect : CameraEffectBase
{
    [SerializeField] bool isActive = false;
    [SerializeField] bool isRevert = false;
    [SerializeField] float fadeSpeed = 10f;
    [SerializeField] float maxValue = 100f;

    [SerializeField] ColorAdjustments colorAdjustment;

    public void OnDisable()
    {
        StopEffect();
    }

    public override void ResetEffect()
    {
        colorAdjustment.saturation.value = 10.0f;
    }

    public override void StartEffect()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustment))
        {
            colorAdjustment.saturation.value = Mathf.MoveTowards(colorAdjustment.saturation.value, -maxValue, fadeSpeed * Time.deltaTime);
            colorAdjustment.active = true;
        }
        isActive = true;
    }

    public override void StopEffect()
    {
        isActive = false;
    }

    public override void RevertEffect()
    {
        colorAdjustment.saturation.value = Mathf.MoveTowards(colorAdjustment.saturation.value, maxValue, fadeSpeed * Time.deltaTime);
    }

    public override void TriggerEffect()
    {
        if (!colorAdjustment) return;
        if (isActive)
        {
            if (isRevert)
                RevertEffect();
            else
            {
                StartEffect();
            }
        }
    }
}
