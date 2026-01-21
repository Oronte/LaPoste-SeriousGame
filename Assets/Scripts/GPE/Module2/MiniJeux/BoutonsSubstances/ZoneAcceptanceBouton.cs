using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ZoneAcceptanceBouton : MonoBehaviour
{
    public event Action OnEnterAccept = null;
    public event Action OnExitAccept = null;

    public event Action OnEnterFail = null;
    public event Action OnExitFail = null;

    [Header("Zones VR")]
    [SerializeField] public Collider acceptZone = null;
    [SerializeField] public Collider failZone = null;

    public bool IsInsideAcceptZone { get; private set; } = false;
    public bool IsInsideFailZone { get; private set; } = false;

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.bounds.Intersects(acceptZone.bounds))
        {
            Debug.Log("Enter Success");
            IsInsideAcceptZone = true;
            OnEnterAccept?.Invoke();
        }

        if(_other.bounds.Intersects(failZone.bounds))
        {
            Debug.Log("Enter Fail");
            IsInsideFailZone = true;
            OnEnterFail?.Invoke();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if(IsInsideAcceptZone && !_other.bounds.Intersects(acceptZone.bounds))
        {
            Debug.Log("Exit Success");
            IsInsideAcceptZone = false;
            OnExitAccept?.Invoke();
        }

        if(IsInsideFailZone && ! _other.bounds.Intersects(failZone.bounds))
        {
            Debug.Log("Exit Fail");
            IsInsideFailZone = false;
            OnExitFail?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (!acceptZone) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(acceptZone.bounds.center, acceptZone.bounds.size);

        if(!acceptZone) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(failZone.bounds.center, failZone.bounds.size);
    }
}