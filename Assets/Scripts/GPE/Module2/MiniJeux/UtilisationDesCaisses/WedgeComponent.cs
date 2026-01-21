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

    void Start()
    {
        Init();
    }

    public void Init()
    {
        grab = GetComponent<XRGrabInteractable>();
        joint = GetComponent<HingeJoint>();
        grab.selectEntered.AddListener(OnSelectEntered); //When a hand interact with this object it calls a function
        grab.selectExited.AddListener(OnSelectExited); //When a hand interact with this object it calls a function
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