using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LetterPile : MonoBehaviour
{
    XRGrabInteractable grab = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IXRSelectInteractor firstHandSelect = null;
    IXRSelectInteractor secondHandSelect = null;
    [SerializeField] bool isGrabbed = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();//Get the component to edit the grab behavior
        grab.trackPosition = false; // The grabbed object will not move or rotate
        grab.trackRotation = false;
        grab.selectEntered.AddListener(OnSelectEntered); //When a hand interact with this object it calls a function
        grab.selectExited.AddListener(OnSelectExit); //When a hand exit interact with this object it calls a function
    }
    
    void OnSelectEntered(SelectEnterEventArgs _args)
    {
        //Interactor is the hand
        //On attribue a firstHandSelect la main qui vient d'intéragir. Si first hand est déjà attribué
        //on attribue alors second hand. Si les deux sont attribués, c'est qu'il y a un problème...
        if (firstHandSelect == null)
        {
            Debug.Log("First hand selected");
            firstHandSelect = _args.interactorObject;
        }
        else if (secondHandSelect == null)
        {
            Debug.Log("Second hand selected");
            secondHandSelect = _args.interactorObject;

            //les deux mains sont détectés. On peut grab
            grab.trackPosition = true;
            grab.trackRotation = true;
            isGrabbed = true;
        }
    }

    void OnSelectExit(SelectExitEventArgs _args)
    {
        //On teste si c'était grab, si oui alors on lâche tout.
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

    void EnableGrab()
    {
        grab.enabled = true;
    }
}