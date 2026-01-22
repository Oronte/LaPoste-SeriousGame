using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurCameraEffect : MonoBehaviour
{ 
    [SerializeField] float fadeSpeed = 10f;

    [SerializeField] Volume volume;
    [SerializeField] ColorAdjustments colorAdjustment;

    private void Start()
    {
        volume = GetComponent<Volume>();
        if(volume.profile.TryGet<ColorAdjustments>(out colorAdjustment))
        {
            Debug.Log("got color adjustement");
            colorAdjustment.active = true;
            
        }
    }

    public void Update()
    {
        if (!colorAdjustment) return;

        DesaturateOverTime();
    }

    public void DesaturateSaturation()
    {
        float _randomSaturation = Random.Range(-100.0f, -40.0f);
        colorAdjustment.saturation.value = _randomSaturation;
    }

    public void ResetSaturation()
    {
        colorAdjustment.saturation.value = 10.0f;
    }

    public void OverSaturate()
    {
        float _randomSaturation = Random.Range(40.0f, 100.0f);
        colorAdjustment.saturation.value = _randomSaturation;
    }

    public void DesaturateOverTime()
    {
        colorAdjustment.saturation.value = Mathf.MoveTowards(colorAdjustment.saturation.value, -100f, fadeSpeed * Time.deltaTime);
    }
}
