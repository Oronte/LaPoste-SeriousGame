using System;
using UnityEngine;

public class ZoneAcceptanceBouton : MonoBehaviour
{
    public event Action OnPressSuccess = null;
    public event Action OnPressFailed = null;

    [Header("Zones")]
    [SerializeField] public Collider acceptZone = null;
    [SerializeField] public Collider failZone = null;

    public void TryPress(bool _hitAccept, bool _hitFail)
    {
        if(_hitAccept && _hitFail)
        {
            Debug.Log("Success");
            OnPressSuccess?.Invoke();
        }
        else
        {
            Debug.Log("Fail");
            OnPressFailed?.Invoke();
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