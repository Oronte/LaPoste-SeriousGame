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
    [SerializeField] ISnapper objectToSnap = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        socket = GetComponentInChildren<Transform>();
        if(!socket)
        {
            Debug.Log("Error Snapper: No Child Found creating a new socket");

            GameObject _newSocket = new GameObject("New Socket");
            _newSocket.transform.parent = transform;
            socket = _newSocket.transform;
            socketInteractor.attachTransform = socket;
        }
    }

    void OnSnap(FCollisionData _collider)
    {

    }

    void OnDesnap(FCollisionData _collider)
    {

    }
}
