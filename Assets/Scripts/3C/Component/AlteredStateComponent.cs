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
    [SerializeField, VisibleAnywhereProperty] Transform rootLeftParent = null;
    [SerializeField, VisibleAnywhereProperty] Transform rootRightParent = null;
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
            Shake(leftHand);
            Shake(rightHand,true);
        }
        if (useLatence)
        {
            //CheckAvailableLatence();
            Latence(rootLeft, rootLeftParent);
            Latence(rootRight, rootRightParent);
        }
        

    }

    void Init()
    {
        inputComponent = GetComponent<InputComponent>();
        if(useLatence)
        {
            Detach(rootLeft, ref rootLeftParent);
            Detach(rootRight, ref rootRightParent);
        }
    }

    //void Inverse()
    //{
    //    if(shouldBePositive)
    //        ComputeNewOffset();
    //    //else
    //    //    SwapVector(ref currentOffset, ref shakeOffset);
    //    shouldBePositive = !shouldBePositive;
    //}

    void Shake(Transform _remote, bool _isReverse = false)
    {
        if (!_remote || !useShaking || !inputComponent)
            return;

        //Debug.Log("Shake");

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

        _remote.localPosition = _isReverse ? -_offset : _offset;
    }

    void Latence(Transform _root, Transform _parent)
    {
        if (!useLatence || !_root ||!_parent) return;

        float _dist = Vector3.Distance(_root.position, _parent.position);

        //if (_dist > tolerance)
        _root.position = Vector3.MoveTowards(_root.position, _parent.position, latenceForce * _dist * Time.deltaTime);
        _root.rotation = _parent.rotation;
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
        //Debug.Log($"New Shake Offset: {_shakeSettings.shakeOffset}");
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

    public void Detach(Transform _root,ref Transform _parent)
    {
        if (!_root) return;
        _parent = _root.parent;
        _root.parent = _parent.parent;
        //Debug.Log("Left hand detached");
    }


}
