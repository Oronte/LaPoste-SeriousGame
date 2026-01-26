using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class ShakeSettings
{
    [Header("Shaking Settings")]

    [SerializeField, Range(0.0f, 1.0f)] public float shakingIntensity;
    [SerializeField] public float shakingFrequency;
    [SerializeField, VisibleAnywhereProperty] public float currentTime;
    [SerializeField, VisibleAnywhereProperty] public Vector3 shakeOffset;
    [SerializeField, VisibleAnywhereProperty] public Vector3 oldSocketOffset;
    [SerializeField] public AnimationCurve curve;

    public ShakeSettings()
    {
        shakingIntensity = 1f;
        shakingFrequency = 0.1f;
        currentTime = 0.0f;
        shakeOffset = Vector3.zero;
        oldSocketOffset = Vector3.zero;
        curve = null;
    }
}

public class AlteredStateComponent : MonoBehaviour
{
    [Header("Global Settings")]

    [SerializeField, VisibleAnywhereProperty] InputComponent inputComponent = null;
    [SerializeField] Transform leftHand = null;
    [SerializeField] Transform rightHand = null;
    [SerializeField] Transform rootLeft = null;
    [SerializeField] Transform rootRight = null;

    [Header("Shaking Settings")]
    [SerializeField] bool useShaking = false;

    //[SerializeField, Range(0.0f, 1.0f)] float shakingIntensity = 1f;
    //[SerializeField] float shakingFrequency = 0.1f;
    //[SerializeField, VisibleAnywhereProperty] float currentTime = 0.0f;
    //[SerializeField, VisibleAnywhereProperty] Vector3 shakeOffset = Vector3.zero;
    //[SerializeField, VisibleAnywhereProperty] Vector3 oldSocketOffset = Vector3.zero;
    //[SerializeField] bool shouldBePositive = false;
    //[SerializeField] AnimationCurve curve = null;

    [SerializeField] List<ShakeSettings> shakeSettings = new List<ShakeSettings>();

    [Header("Latence Settings")]

    [SerializeField] bool useLatence = false;
    [SerializeField] float latenceForce = 0.0f;
    //[SerializeField] float tolerance = 0.01f;
    [SerializeField, VisibleAnywhereProperty] Transform rootRightParent = null;
    [SerializeField, VisibleAnywhereProperty] Transform rootLeftParent = null;
    //[SerializeField, VisibleAnywhereProperty] float currentDelayTime = 0.0f, maxDelayTime = 0.0f;
    //[SerializeField] float minDelay = 0.005f, MaxDelay = 0.02f;
    //[SerializeField] bool useTimerDelay = false;
    //[SerializeField] bool remoteCanMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (useShaking)
        {
            //UpdateTime(ref currentTime, shakingFrequency, ComputeNewOffset);
            Shake();
        }
        if (useLatence)
        {
            //CheckAvailableLatence();
            Latence();
        }
        

    }

    void Init()
    {
        inputComponent = GetComponent<InputComponent>();
        if(useLatence)
            Detach();
    }

    //void Inverse()
    //{
    //    if(shouldBePositive)
    //        ComputeNewOffset();
    //    //else
    //    //    SwapVector(ref currentOffset, ref shakeOffset);
    //    shouldBePositive = !shouldBePositive;
    //}

    void Shake()
    {
        if (!useShaking || !leftHand || !rightHand || !inputComponent)
            return;

        Debug.Log("Shake");

        Vector3 _offset = Vector3.zero;
        int _size = shakeSettings.Count;
        for (int i = 0; i < _size; i++)
        {
            ShakeSettings _shakeSetting = shakeSettings[i];
            UpdateTime(ref _shakeSetting.currentTime, _shakeSetting.shakingFrequency,() => {ComputeNewOffset(ref _shakeSetting); });

            float _curveResult = _shakeSetting.curve.Evaluate(_shakeSetting.currentTime / _shakeSetting.shakingFrequency);
            _offset += Vector3.Lerp(_shakeSetting.oldSocketOffset, _shakeSetting.shakeOffset, _curveResult);
            shakeSettings[i] = _shakeSetting;
        }

        //if(curve == null)
        //    leftHand.localPosition = Vector3.Lerp(currentOffset, shakeOffset, currentTime / shakingFrequency) * Time.deltaTime;
        //else

        leftHand.localPosition = _offset;
    }

    void Latence()
    {
        if (!useLatence /*|| !remoteCanMove*/) return;

        float _dist = Vector3.Distance(rootLeft.position, rootLeftParent.position);

        //if (_dist > tolerance)
        rootLeft.position = Vector3.MoveTowards(rootLeft.position, rootLeftParent.position, latenceForce * _dist * Time.deltaTime);
        rootLeft.rotation = rootLeftParent.rotation;
        //else
        //{
        //    remoteCanMove = false;
        //}
    }

    //void CheckAvailableLatence()
    //{
    //    if (!useLatence) return;
    //    if(useTimerDelay)
    //    {
    //        UpdateTime(ref currentDelayTime, maxDelayTime, ResetTimerDelay);
    //    }
    //    else if(!remoteCanMove && Vector3.Distance(rootLeft.position, rootLeftParent.position) > tolerance)
    //    {
    //        useTimerDelay = true;
    //    }
    //}
    
    //void ResetTimerDelay()
    //{
    //    useTimerDelay = false;
    //    remoteCanMove = true;
    //    currentDelayTime = 0.0f;
    //    maxDelayTime = UnityEngine.Random.Range(minDelay, MaxDelay);
    //}

    void ComputeNewOffset(ref ShakeSettings _shakeSettings)
    {
        float _skakingIntensity = _shakeSettings.shakingIntensity / 100;

        float _x = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;
        float _y = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;
        float _z = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;

        _shakeSettings.oldSocketOffset = _shakeSettings.shakeOffset;
        _shakeSettings.shakeOffset = new Vector3(_x, _y, _z);
        Debug.Log($"New Shake Offset: {_shakeSettings.shakeOffset}");
    }

    void SwapVector(ref Vector3 _a,ref Vector3 _b)
    {
        Vector3 _temp = _a;
        _a = _b;
        _b = _temp;
    }

    void UpdateTime(ref float _currentTime, float _maxTime, Action _action = null)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _maxTime)
        {
            _currentTime = 0.0f;
            _action?.Invoke();
        }
    }

    public void Detach()
    {
        if (!leftHand || !rightHand || !rootLeft || !rootRight) return;
        rootLeftParent = rootLeft.parent;
        rootLeft.parent = rootLeftParent.parent;
        Debug.Log("Left hand detached");
    }


}
