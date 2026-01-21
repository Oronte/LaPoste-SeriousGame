using System.ComponentModel;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LetterPileComponent : MonoBehaviour
{
    [Header("Grab", order = 0)]
    [SerializeField, VisibleAnywhereProperty] XRGrabInteractable grab = null; //Grab component. Useful to manage if the object can be grab
    public XRGrabInteractable Grab => grab; //Accessor
    IXRSelectInteractor firstHandSelect = null; //the interface that represents the hand in XR
    IXRSelectInteractor secondHandSelect = null;
    [SerializeField, VisibleAnywhereProperty] bool isGrabbed = false; //If the pile is grabbed

    [Header("Components", order = 1)]
    [SerializeField] BoxCollider ke7BoxCollider = null; //The collider of the K&7 (caissette)
    [SerializeField] RadialProgressBar progressBar = null;

    [Header("Debug", order = 2)]
    [SerializeField] bool useDebug = false;

    void Start()
    {
        Init();
    }
    
    public void Init()
    {
        grab = GetComponent<XRGrabInteractable>();//Get the component to edit the grab behavior

        if (grab)
        {
            grab.trackPosition = false; // The grabbed object will not move or rotate
            grab.trackRotation = false;
            grab.selectEntered.AddListener(OnSelectEntered); //When a hand interact with this object it calls a function
            grab.selectExited.AddListener(OnSelectExited); //When a hand exit interact with this object it calls a function
        }
        
        if (progressBar)
        {
            progressBar.OnMinValue.AddListener(OnMinValueReached); //Add event when the min value is reached, it means that the player lost
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        //When a collision is trigger, check if it's the box to validate the position
        if (_collision.collider == ke7BoxCollider)
        {
            if (useDebug) Debug.Log("The pile is in the trigger zone");
            if (progressBar)
            {
                progressBar.Activate = true;
            }
        }
    }

    private void OnCollisionExit(Collision _collision)
    {
        if (_collision.collider == ke7BoxCollider)
        {
            if (useDebug) Debug.Log("The pile is no longer in the trigger zone");
            progressBar.Activate = false; //If it's not activate, progress will decrement. It starts at 100%
        }
    }

    void OnSelectEntered(SelectEnterEventArgs _args)
    {
        //We assign firstHandSelect to the hand that just interacted. If first hand is already assigned,
        //then we assign second hand. If both are assigned, there is a problem...
        if (firstHandSelect == null)
        {
            if (useDebug) Debug.Log("First hand selected");
            firstHandSelect = _args.interactorObject;
        }
        else if (secondHandSelect == null)
        {
            if (useDebug) Debug.Log("Second hand selected");
            secondHandSelect = _args.interactorObject;

            //Both hands are detected. We can grab.
            grab.trackPosition = true;
            grab.trackRotation = true;
            isGrabbed = true;
        }
    }

    void OnSelectExited(SelectExitEventArgs _args)
    {
        //We test if it was grab, if so then we drop everything.
        if (isGrabbed)
        {
            grab.enabled = false;
            isGrabbed = false;
            grab.trackPosition = false;
            grab.trackRotation = false;
            Invoke(nameof(EnableGrab), 1.0f);
            firstHandSelect = null;
            secondHandSelect = null;
        }
    }

    public void DisableGrab()
    {
        grab.enabled = false;
    }

    public void EnableGrab()
    {
        grab.enabled = true;
    }

    void OnMinValueReached()
    {
        if (useDebug) Debug.Log("Min value have been reached");
    }
}