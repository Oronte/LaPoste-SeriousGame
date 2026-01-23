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
    bool isUnsnap = true, isSnap = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        socket = GetComponentInChildren<Transform>();
        socketInteractor = GetComponent<XRSocketInteractor>();
        if (socketInteractor.attachTransform == null) return;
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
        
    }

    public void OnSnap()
    {
        foreach (GameObject _current in enablers)
        {
            if(_current == null) continue ;
            _current.GetComponent<IEnabler>().Enable();
        }
    }

    public void OnUnSnap()
    {
        foreach (GameObject _current in enablers)
        {
            if (_current == null) continue;
            _current.GetComponent<IEnabler>().Disable();
        }
    }
}
