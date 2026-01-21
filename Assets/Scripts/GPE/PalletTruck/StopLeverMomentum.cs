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
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
