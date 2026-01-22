using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(HingeJoint), typeof(XRGrabInteractable))]
public class LeverComponent : MonoBehaviour, IEnabler
{

    public event Action onPulling = null;
    public event Action onResting = null;
    public event Action onMinAngle = null;
    public event Action onMaxAngle = null;


    [SerializeField] HingeJoint hinge;
    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField, Range(0f, 1f)] float minTolerance = 0.1f, maxTolerance = 0.8f;
    [SerializeField] List<GameObject> enablers = new List<GameObject>();

    JointLimits limits;
    JointSpring spring;
    bool useLimit = true, useMotor = false, isSettle = false;

    // Interface implementation 
    [SerializeField] public bool IsEnable { get; set; } = true;

    public float ComputeAngleToPourcentage(float angle)
    {
        return (angle - hinge.limits.min) / (hinge.limits.max - hinge.limits.min);
    }

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
            Debug.Log("Error Levier Component: No XRGrabInteractable found");

        hinge = GetComponent<HingeJoint>();
        if (hinge == null)
            Debug.Log("Error Levier Component: No hinge joint found");
        limits = hinge.limits;
        spring = hinge.spring;
        useLimit = hinge.useLimits;
        useMotor = hinge.useMotor;
        UpdateCanMove(IsEnable);

    }


    private void Update()
    {
        if (!IsEnable || !hinge) return;

        float _angleInPourcentage = ComputeAngleToPourcentage(hinge.angle);

        if (!isSettle && _angleInPourcentage <= minTolerance)
        {
            Debug.Log("Min Angle");
            onMinAngle?.Invoke();
            isSettle = true;
            foreach (GameObject _current in enablers)
            {
                _current.GetComponent<IEnabler>().Enable();
            }
            return;
        }

        if (!isSettle && _angleInPourcentage >= maxTolerance)
        {
            Debug.Log("Max Angle");
            onMaxAngle?.Invoke();
            isSettle = true;
            foreach (GameObject _current in enablers)
            {
                _current.GetComponent<IEnabler>().Disable();
            }
            return;
        }

        if (isSettle && _angleInPourcentage >= minTolerance && _angleInPourcentage <= maxTolerance)
        {
            Debug.Log("Resting");
            onResting?.Invoke();
            isSettle = false;
            return;
        }
    }

    public void UpdateCanMove(bool _canMoveState)
    {
        if (!grabInteractable) return;
        IsEnable = _canMoveState;
        if (IsEnable)
            Enable();
        else
            Disable();
    }


    public void Enable()
    {

        if (!grabInteractable) return;
        if (GetComponent<HingeJoint>()) return;
        Debug.Log("Enable");

        hinge = gameObject.AddComponent<HingeJoint>();
        hinge.limits = limits;
        hinge.spring = spring;
        hinge.useLimits = useLimit;
        hinge.useMotor = useMotor;
        grabInteractable.enabled = true;
        IsEnable = true;
    }


    public void Disable()
    {
        Debug.Log("Disable");

        if (!grabInteractable) return;
        if (!GetComponent<Joint>()) return;
        Destroy(hinge);
        hinge = null;
        grabInteractable.enabled &= false;
        IsEnable = true;
    }

    //private void ToSnap(float _angleToSnap)
    //{
    //    float _angleInPourcentage = ComputeAngleToPourCentage(_angleToSnap);
    //    float _degreeRotation = hinge.limits.min + (hinge.limits.max - hinge.limits.min) * _angleInPourcentage;
    //    Vector3 _worldSpaceHingeAxis = transform.TransformDirection(hinge.axis);
    //    transform.rotation = Quaternion.AngleAxis(_degreeRotation, _worldSpaceHingeAxis) * transform.rotation;

    //}
}
