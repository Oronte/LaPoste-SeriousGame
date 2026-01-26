using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WedgeComponent : MonoBehaviour
{
    [SerializeField, VisibleAnywhereProperty] XRGrabInteractable grab = null;
    [SerializeField, VisibleAnywhereProperty] HingeJoint joint = null;
    [SerializeField, VisibleAnywhereProperty] LetterPileComponent letterPile = null;
    [SerializeField, VisibleAnywhereProperty] bool isGrabbed = false;//La cale a été grab
    [SerializeField, VisibleAnywhereProperty] bool isRemoved = false;//La cale a été relevé ?
    public bool IsRemoved { get => isRemoved; set => isRemoved = value; }

    [SerializeField, VisibleAnywhereProperty] Vector3 defaultPosition = Vector3.zero;
    [SerializeField, VisibleAnywhereProperty] Quaternion defaultRotation = Quaternion.identity;

    void Start()
    {
        Init(); //TODO Remove, the storage letter game will do it
    }

    public void Init(XRGrabInteractable _grabComponent = null)
    {
        grab = _grabComponent ? _grabComponent : GetComponent<XRGrabInteractable>(); //TODO Change when MiniGame is ready
        joint = GetComponent<HingeJoint>();
        grab.selectEntered.AddListener(OnSelectEntered); //When a hand interact with this object it calls a function
        grab.selectExited.AddListener(OnSelectExited); //When a hand interact with this object it calls a function
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    public void Reset()
    {
        //Reset all parameters to restart the game
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
        //Check if the wedge is at around 90°. If true, the player can grab the pile of letters
        if (!isGrabbed) return;
        if (joint.angle >= 89f)
        {
            grab.trackPosition = false;
            letterPile.Grab.enabled = true; //Enable the grab component on the letterPile
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