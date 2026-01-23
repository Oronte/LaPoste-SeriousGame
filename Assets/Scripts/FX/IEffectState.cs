using UnityEngine;

public interface IEffectState
{
    public void ResetEffect();
    public void StartEffect();
    public void StopEffect();
    public void RevertEffect();
}
