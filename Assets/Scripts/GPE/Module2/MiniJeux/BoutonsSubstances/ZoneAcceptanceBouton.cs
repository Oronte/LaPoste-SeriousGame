using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ZoneAcceptanceBouton : MonoBehaviour
{
    public event Action OnEnterAccept = null;
    public event Action OnExitAccept = null;

    public event Action OnEnterFail = null;
    public event Action OnExitFail = null;

    [Header("References")]
    [SerializeField] Collider acceptZone = null;
    [SerializeField] Collider failZone = null;

    public bool IsInsideAcceptZone { get; private set; } = false;
    public bool IsInsideFailZone { get; private set; } = false;

    private void OnTriggerEnter(Collider _other)
    {
        if(_other == acceptZone)
        {
            IsInsideAcceptZone = true;
            OnEnterAccept?.Invoke();
            Debug.Log("Enter Success");
        }
        else if(_other == failZone)
        {
            IsInsideFailZone = true;
            OnEnterFail?.Invoke();
            Debug.Log("Enter Fail");
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if(_other == acceptZone)
        {
            IsInsideAcceptZone = false;
            OnExitAccept?.Invoke();
            Debug.Log("Exit Success");
        }
        else if(_other == failZone)
        {
            IsInsideFailZone = false;
            OnExitFail?.Invoke();
            Debug.Log("Exit Fail");
        }
    }

    private void OnDrawGizmos()
    {
        if (!acceptZone) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(acceptZone.bounds.center, acceptZone.bounds.size);

        if (!failZone) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(failZone.bounds.center, failZone.bounds.size);
    }
}