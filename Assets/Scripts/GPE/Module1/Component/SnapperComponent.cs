using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

//
// Summary:
//     Component used to snap object to an another using the triggerbox component
[RequireComponent(typeof(XRSocketInteractor))]
public class SnapperComponent : MonoBehaviour
{

    [SerializeField] Transform socket = null;
    [SerializeField] XRSocketInteractor socketInteractor = null;
    [SerializeField] List<GameObject> enablers = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        socket = GetComponentInChildren<Transform>();
        if(!socketInteractor.attachTransform) return;
        {
            if (!socket)
            {
                Debug.Log("Error Snapper: No Child Found creating a new socket");

                GameObject _newSocket = new GameObject("New Socket");
                _newSocket.transform.parent = transform;
                socket = _newSocket.transform;
            }
            socketInteractor.attachTransform = socket;
        }
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void FixedUpdate()
    {
        
        
    }
    void OnSnap(FCollisionData _collider)
    {
        foreach (GameObject _newSocket in enablers)
        {
            IEnabler _enabler = _newSocket.GetComponent<IEnabler>();
            if (_enabler == null) continue;
            _enabler.Enable();
        }
    }

    void OnDesnap(FCollisionData _collider)
    {
        foreach (GameObject _newSocket in enablers)
        {
            IEnabler _enabler = _newSocket.GetComponent<IEnabler>();
            if (_enabler == null) continue;
            _enabler.Disable();
        }
    }
}
