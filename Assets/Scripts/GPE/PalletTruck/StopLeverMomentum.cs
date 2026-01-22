using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StopLeverMomentum : MonoBehaviour
{
    private Rigidbody rb;
    public XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.freezeRotation = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.freezeRotation = true;

        float _currentAngle = transform.localEulerAngles.z;
        if (_currentAngle > 180.0f) _currentAngle -= 360.0f;

        if (Mathf.Abs(_currentAngle) < 10.0f)
        {
            Vector3 _targetRotation = transform.localEulerAngles;
            _targetRotation.z = 0;
            transform.localEulerAngles = _targetRotation;
        }
    }
}
