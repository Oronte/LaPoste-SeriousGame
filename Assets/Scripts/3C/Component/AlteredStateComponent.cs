using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AlteredStateComponent : MonoBehaviour
{
    [Header("Global Settings")]

    [SerializeField, VisibleAnywhereProperty] InputComponent inputComponent = null;
    [SerializeField] Transform leftHand = null;
    [SerializeField] Transform rightHand = null;
    [SerializeField] Transform rootLeft = null;
    [SerializeField] Transform rootRight = null;

    [Header("Shaking Settings")]

    [SerializeField, Range(0.0f, 1.0f)] float shakingIntensity = 1f;
    [SerializeField] float shakingFrequency = 0.1f;
    [SerializeField, VisibleAnywhereProperty] float currentTime = 0.0f;
    [SerializeField, VisibleAnywhereProperty] Vector3 shakeOffset = Vector3.zero;
    [SerializeField, VisibleAnywhereProperty] Vector3 oldSocketOffset = Vector3.zero;
    [SerializeField] bool shouldBePositive = false;
    [SerializeField] bool useShaking = false;
    [SerializeField] AnimationCurve curve = null;

    [Header("Latence Settings")]

    [SerializeField] bool useLatence = false;
    [SerializeField] float maxDistancedDelta = 0.0f;
    [SerializeField, VisibleAnywhereProperty] Transform rootRightParent = null;
    [SerializeField, VisibleAnywhereProperty] Transform rootLeftParent = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (useShaking)
        {
            UpdateTime(ref currentTime, shakingFrequency, ComputeNewOffset);
            Shake();
        }
        if (useLatence)
        {
            Latence();
        }

    }

    void Init()
    {
        inputComponent = GetComponent<InputComponent>();
        if(useLatence)
            Detach();
    }

    void Inverse()
    {
        if(shouldBePositive)
            ComputeNewOffset();
        //else
        //    SwapVector(ref currentOffset, ref shakeOffset);
        shouldBePositive = !shouldBePositive;
    }

    void Shake()
    {
        if (!useShaking || !leftHand || !rightHand || !inputComponent)
            return;

        Debug.Log("Shake");

        //if(curve == null)
        //    leftHand.localPosition = Vector3.Lerp(currentOffset, shakeOffset, currentTime / shakingFrequency) * Time.deltaTime;
        //else

        float _curveResult = curve.Evaluate(currentTime / shakingFrequency);
        Vector3 _offset = Vector3.Lerp(oldSocketOffset, shakeOffset, _curveResult);

        leftHand.localPosition = _offset;
    }

    void Latence()
    {
        if (!useLatence) return;
        rootLeft.position = Vector3.MoveTowards(rootLeft.position, rootLeftParent.position, maxDistancedDelta);
    }

    void ComputeNewOffset()
    {
        float _skakingIntensity = shakingIntensity / 100;

        float _x = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;
        float _y = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;
        float _z = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;

        oldSocketOffset = shakeOffset;
        shakeOffset = new Vector3(_x, _y, _z);
        Debug.Log($"New Shake Offset: {shakeOffset}");
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
