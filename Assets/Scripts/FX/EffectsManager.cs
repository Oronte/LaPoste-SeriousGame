using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.Rendering;
using System;

[Serializable]
public struct FEffectEntry
{
    public CameraEffectBase effect;
    public float duration;
    public float startDelay;

    public FEffectEntry(CameraEffectBase _effect, float _duration, float _delay = 0f)
    {
        effect = _effect; 
        duration = _duration;
        startDelay = _delay;
    }
}

public class EffectsManager : Singleton<EffectsManager>
{
    [SerializeField] List<FEffectEntry> effectsSequence = new List<FEffectEntry>();

    Volume volume;

    private void Start()
    {
        volume = GetComponent<Volume>();
        //AddEffect<SaturateCameraEffect>(20f, 5f);
    }

    public void AddEffect<T>(float _duration, float _startDelay = 0f) where T: CameraEffectBase, new()
    {
        T _effect = new T();
        _effect.Init(volume);
        FEffectEntry _entry = new FEffectEntry(_effect, _duration, _startDelay);
        effectsSequence.Add(_entry);
        _effect.StartEffect();
    }

    public void PlayAllEffects()
    {
        List<FEffectEntry> _garbageCollector = new List<FEffectEntry>();
        int _count = effectsSequence.Count;
        for(int _i = 0; _i < _count; ++_i)
        {
            FEffectEntry _entry = effectsSequence[_i];

            if(_entry.startDelay > 0)
            {
                _entry.startDelay -= Time.deltaTime;
                effectsSequence[_i] = _entry;

                continue;
            }
            _entry.effect.TriggerEffect();
            _entry.duration -= Time.deltaTime;

            effectsSequence[_i] = _entry;

            if (_entry.duration <= 0)
                _garbageCollector.Add(_entry);
        }

        // Remove expired effects
       effectsSequence = effectsSequence.Except(_garbageCollector).ToList();
        foreach(FEffectEntry _entry in _garbageCollector)
        {
            _entry.effect.ResetEffect();
            _entry.effect.StopEffect();
        }
    }

    private void Update()
    {
        PlayAllEffects();
    }

    //private IEnumerator RunEffectRoutine(EffectEntry entry)
    //{
    //    if(entry.startDelay > 0)
    //        yield return new WaitForSeconds(entry.startDelay);

    //    entry.effect.StartEffect();

    //    if(entry.activeDuration > 0)
    //        yield return new WaitForSeconds(entry.activeDuration);

    //    entry.effect.StopEffect();
    //}

    public void StopAll() => effectsSequence.Clear();

    public void ResetAll()
    {
        foreach (var entry in effectsSequence)
        {
            entry.effect.ResetEffect();
        }
    }

}
