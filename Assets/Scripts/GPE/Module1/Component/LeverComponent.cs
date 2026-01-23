using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
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
    [SerializeField] bool isEnable = true;

    bool isSettle = false;

    // Interface implementation 
    public bool IsEnable { get { return isEnable; } set { isEnable = value; } }

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

        UpdateCanMove(IsEnable);

    }


    private void FixedUpdate()
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
                if (_current == null) continue;
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
                if (_current == null) continue;
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
        grabInteractable.enabled = true;
        IsEnable = true;
    }


    public void Disable()
    {
        Debug.Log("Disable");
        grabInteractable.enabled &= false;
        IsEnable = false;
    }

    //private void ToSnap(float _angleToSnap)
    //{
    //    float _angleInPourcentage = ComputeAngleToPourCentage(_angleToSnap);
    //    float _degreeRotation = hinge.limits.min + (hinge.limits.max - hinge.limits.min) * _angleInPourcentage;
    //    Vector3 _worldSpaceHingeAxis = transform.TransformDirection(hinge.axis);
    //    transform.rotation = Quaternion.AngleAxis(_degreeRotation, _worldSpaceHingeAxis) * transform.rotation;

    //}
}
