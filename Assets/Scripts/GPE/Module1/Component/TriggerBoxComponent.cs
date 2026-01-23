using System;
using UnityEngine;

[Serializable]
public struct FCollisionData
{
    public GameObject objectCollision;
    public Vector3 hitPos;


    public FCollisionData(GameObject _objectCollision, Vector3 _hitPos)
    {
        objectCollision = _objectCollision;
        hitPos = _hitPos;
    }
}

[RequireComponent(typeof(BoxCollider))]
public class TriggerBoxComponent : MonoBehaviour
{
    public event Action<FCollisionData> OnEnter = null;
    public event Action<FCollisionData> OnExit = null;

    [SerializeField] BoxCollider boxCollider = null;
    [SerializeField] LayerMask layers = new LayerMask();

    bool isTriggered = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.Log("Error TriggerBox: No Collider found");
            return;
        }
        boxCollider.isTrigger = true;
        boxCollider.providesContacts = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (((1 << collider.gameObject.layer) & layers) == 0) return;

        isTriggered = true;
        FCollisionData _newData = new FCollisionData(collider.gameObject, collider.gameObject.transform.position);
        OnEnter?.Invoke(_newData);
    }
    private void OnTriggerExit(Collider collider)
    {
        if (((1 << collider.gameObject.layer) & layers) == 0) return;

        isTriggered = false;
        FCollisionData _newData = new FCollisionData(collider.gameObject, collider.gameObject.transform.position);
        OnExit?.Invoke(_newData);

    }

    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;
        Gizmos.color = isTriggered ? Color.green : Color.red;
        Vector3 _boxColliderSize = boxCollider.size;
        Vector3 _localScale = transform.localScale;
        Gizmos.DrawWireCube(transform.position, new Vector3(_boxColliderSize.x * _localScale.x,
                                                            _boxColliderSize.y * _localScale.y,
                                                            _boxColliderSize.z * _localScale.z));
        Gizmos.color = Color.white;
    }

}


