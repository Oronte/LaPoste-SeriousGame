using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TB_StopButton : MonoBehaviour
{
    BatterieTimingGame owner;
    XRSimpleInteractable interactable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(BatterieTimingGame _owner)
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.activated.AddListener(_owner.StopRedBar);

        owner = _owner;

    }


    public void TestEvent(ActivateEventArgs _agrs)
    {
        Debug.Log("ca marche !");

    }
}
