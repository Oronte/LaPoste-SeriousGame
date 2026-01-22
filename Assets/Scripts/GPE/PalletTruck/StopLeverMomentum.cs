using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StopLeverMomentum : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.freezeRotation = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.freezeRotation = true;
        float _currentAngle = transform.localEulerAngles.z;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (_currentAngle > 180.0f) _currentAngle -= 360.0f;

        if (_currentAngle < 5.0f && _currentAngle > -5.0f) transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }
}
