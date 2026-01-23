using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public abstract class CameraEffectBase
{
    protected Volume volume = null;

    public void Init(Volume _volume)
    {
        volume = _volume;
    }

    public abstract void ResetEffect();
    public abstract void StartEffect();
    public abstract void StopEffect();
    public abstract void RevertEffect();
    public abstract void TriggerEffect();


}
