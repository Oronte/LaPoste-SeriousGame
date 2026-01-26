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
    [SerializeField, VisibleAnywhereProperty] BoxCollider ke7BoxCollider = null; //The collider of the K&7 (caissette)
    [SerializeField, VisibleAnywhereProperty] RadialProgressBar progressBar = null; //The progress bar

    [Header("Debug", order = 2)]
    [SerializeField] bool useDebug = false;

    [Header("Component Settings", order = 3)]
    [SerializeField, VisibleAnywhereProperty] bool isGameOver = false;
    [SerializeField, VisibleAnywhereProperty] Vector3 defaultPosition = Vector3.zero;
    [SerializeField, VisibleAnywhereProperty] Quaternion defaultRotation = Quaternion.identity;

    void Start()
    {
        Init();//TODO Remove, the storage letter game will do it
    }
    
    public void Init(RadialProgressBar _progressBar = null)
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        grab = GetComponent<XRGrabInteractable>();//Get the component to edit the grab behavior
        if (_progressBar) progressBar = _progressBar;
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

    public void Reset()
    {
        ChangeGameStatusGame(false);
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        progressBar.Activate = true;
        isGameOver = false;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (isGameOver) return;
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
        if (isGameOver) return;
        if (_collision.collider == ke7BoxCollider)
        {
            if (useDebug) Debug.Log("The pile is no longer in the trigger zone");
            progressBar.Activate = false; //If it's not activate, progress will decrement. It starts at 100%
        }
    }

    void OnSelectEntered(SelectEnterEventArgs _args)
    {
        if (isGameOver) return;
        //We assign firstHandSelect to the hand that just interacted. If first hand is already assigned,
        //then we assign second hand. If both are assigned, there is a problem...
        if (firstHandSelect == null)
        {
            if (useDebug) Debug.Log("First hand selected");
            firstHandSelect = _args.interactorObject;
        }
        else if (secondHandSelect == null && _args.interactorObject != firstHandSelect)
        {
            if (useDebug) Debug.Log("Second hand selected");
            secondHandSelect = _args.interactorObject;

            ChangeGameStatusGame(true);
            if (progressBar)
            {
                progressBar.Activate = true;
            }
        }
    }

    void ChangeGameStatusGame(bool _status = true)
    {
        grab.trackPosition = _status;
        grab.trackRotation = _status;
        grab.enabled = _status;
        isGrabbed = _status;
    }

    void OnSelectExited(SelectExitEventArgs _args)
    {
        if (isGameOver) return;
        //We test if it was grab, if so then we drop everything.
        if (isGrabbed)
        {
            ChangeGameStatusGame(false);

            Invoke(nameof(EnableGrab), 1.0f); // Timer to disable grab mod for x time (if a hands let go the letters it will fall)
            firstHandSelect = null;
            secondHandSelect = null;

            //Just in case, if we let go of the letters, it might stop the game.
            //progressBar.OnMinValue.Invoke();

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
        isGameOver = true;
        ChangeGameStatusGame(false);
    }
}