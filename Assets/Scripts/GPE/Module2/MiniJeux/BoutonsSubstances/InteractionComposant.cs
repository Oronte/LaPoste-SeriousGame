using UnityEngine;

public class InteractionComposant : MonoBehaviour
{
    [SerializeField] float maxDistance = 10f;
    [SerializeField] LayerMask interactableLayer = 0;
    [SerializeField] ZoneAcceptanceBouton zoneAcceptance = null;

    Ray screenRay = new Ray();

    void Update()
    {
        screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool _hitAccept = false, _hitFail = false;

        RaycastHit[] _hits = Physics.RaycastAll(screenRay, maxDistance, interactableLayer);

        foreach (RaycastHit _hit in _hits)
        {
            if (_hit.collider == zoneAcceptance.acceptZone)
            {
                _hitAccept = true;
            }
            if(_hit.collider == zoneAcceptance.failZone)
            {
                _hitFail = true;
            }
        }
        zoneAcceptance.TryPress(_hitAccept, _hitFail);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(screenRay.origin, screenRay.direction * maxDistance);
    }
}