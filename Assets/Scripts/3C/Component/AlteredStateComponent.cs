using System;
using UnityEngine;

public class AlteredStateComponent : MonoBehaviour
{
    [SerializeField, VisibleAnywhereProperty] InputComponent inputComponent = null;
    [SerializeField] Transform leftHand = null;
    [SerializeField] Transform rightHand = null;

    [Header ("Shaking Settings")]
    [SerializeField, Range(0.0f, 1.0f)] float shakingIntensity = 1f;
    [SerializeField] float shakingFrequency = 0.1f;
    [SerializeField, VisibleAnywhereProperty] float currentTime = 0.0f;
    [SerializeField, VisibleAnywhereProperty] Vector3 shakeOffset = Vector3.zero;
    [SerializeField, VisibleAnywhereProperty] Vector3 currentOffset = Vector3.zero;
    [SerializeField] bool shouldBePositive = false;
    [SerializeField] bool useShaking = false;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (useShaking)
        {
            UpdateTime(ref currentTime, shakingFrequency, Inverse);
            Shake();
        }

    }

    void Init()
    {
        inputComponent = GetComponent<InputComponent>();
    }

    void Inverse()
    {
        if(shouldBePositive)
            ComputeNewOffset();
        else
            SwapVector(ref currentOffset, ref shakeOffset);
        shouldBePositive = !shouldBePositive;
    }

    void Shake()
    {
        if (!useShaking || !leftHand || !rightHand || !inputComponent)
            return;

        Debug.Log("Shake");
        leftHand.localPosition += Vector3.Lerp(currentOffset, shakeOffset, currentTime / shakingFrequency) * Time.deltaTime;
    }

    void ComputeNewOffset()
    {
        float _skakingIntensity = shakingIntensity / 10;

        float _x = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;
        float _y = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;
        float _z = UnityEngine.Random.Range(-1f, 1f) * _skakingIntensity;

        shakeOffset = new Vector3(_x, _y, _z);
        currentOffset = Vector3.zero;
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
}
