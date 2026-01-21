using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Levier : MonoBehaviour
{

    public event Action onPulling = null;
    public event Action<bool> onCorrectAngle = null;

    [SerializeField] public Transform attachSocket = null;
    [SerializeField] public XRGrabInteractable interactableRef = null;
    [SerializeField] public float minAngle = 0.0f, maxAngle = 0.0f, snapAngleTolerance = 5f, sensitivity = 0.0f;
    public bool isGrabbed = false, isInCorrectAngle = false;
    Transform interactor = null;



    private void Awake()
    {
        interactableRef.selectEntered.AddListener(OnGrab);
        interactableRef.selectExited.AddListener(OnRelease);
    }

    public void OnGrab(SelectEnterEventArgs _args)
    {
        isGrabbed = true;
        interactor = _args.interactorObject.transform;
    }
    private void OnRelease(SelectExitEventArgs _args)
    {
        isGrabbed = false;
        interactor = null;
    }


    private void Update()
    {
        if (isGrabbed && interactor != null)
        {
            onPulling?.Invoke();
            Vector3 localDirection = attachSocket.InverseTransformPoint(interactor.position);
            float _deltaY = localDirection.y;
            float _targetAngle = _deltaY * sensitivity;
            _targetAngle = Mathf.Clamp(_targetAngle, minAngle, maxAngle);

            attachSocket.localRotation = Quaternion.Euler(_targetAngle, 0f, 0f);
      
        }
        float _currentAngle = attachSocket.localEulerAngles.x;
        if (_currentAngle > 180f) _currentAngle -= 360f;

        float _clampedAngle = Mathf.Clamp(_currentAngle, minAngle, maxAngle);
        attachSocket.localRotation = Quaternion.Euler(_clampedAngle, 0f, 0f);

        if ((_currentAngle >= maxAngle - snapAngleTolerance) ||
          (_currentAngle <= minAngle + snapAngleTolerance))
        {
            isInCorrectAngle = true;
        }
        else
        {
            isInCorrectAngle = false;

        }
        onCorrectAngle?.Invoke(isInCorrectAngle);
    }
}
