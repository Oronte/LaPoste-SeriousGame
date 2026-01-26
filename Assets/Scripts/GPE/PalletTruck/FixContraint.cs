using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FixContraint : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    public RigidbodyConstraints contraint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
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
        rb.constraints = RigidbodyConstraints.None;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.constraints = contraint;
    }
}
