using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WedgeComponent : MonoBehaviour
{
    [SerializeField, VisibleAnywhereProperty] XRGrabInteractable grab = null;
    [SerializeField, VisibleAnywhereProperty] HingeJoint joint = null;
    [SerializeField] LetterPileComponent letterPile = null;
    [SerializeField] bool isGrabbed = false;//La cale a été grab
    [SerializeField] bool isRemoved = false;//La cale a été relevé ?
    public bool IsRemoved { get => isRemoved; set => isRemoved = value; }

    [SerializeField] Vector3 defaultPosition = Vector3.zero;
    [SerializeField] Quaternion defaultRotation = Quaternion.identity;
    void Start()
    {
        Init(); //TODO Remove, the storage letter game will do it
    }

    public void Init()
    {
        grab = GetComponent<XRGrabInteractable>();
        joint = GetComponent<HingeJoint>();
        grab.selectEntered.AddListener(OnSelectEntered); //When a hand interact with this object it calls a function
        grab.selectExited.AddListener(OnSelectExited); //When a hand interact with this object it calls a function
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    public void Reset()
    {
        enabled = true;
        isGrabbed = false;
        isRemoved = false;
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
    }

    private void Update()
    {
        CheckIfIsOpen();
    }

    void CheckIfIsOpen()
    {
        if (!isGrabbed) return;
        if (joint.angle >= 89f)
        {
            Debug.Log("Angle is ok");
            grab.trackPosition = false;
            letterPile.Grab.enabled = true;
            isRemoved = true;
            enabled = false;
            
        }
    }

    void OnSelectEntered(SelectEnterEventArgs _args)
    {
        isGrabbed = true;
    }

    void OnSelectExited(SelectExitEventArgs _args)
    {
        isGrabbed = false;
    }
}