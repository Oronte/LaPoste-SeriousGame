using UnityEngine;

[System.Serializable]
public class EffectEntry
{
    public IEffectState effect;

    public float startDelay = 0f;

    public float activeDuration = 1f;

}
