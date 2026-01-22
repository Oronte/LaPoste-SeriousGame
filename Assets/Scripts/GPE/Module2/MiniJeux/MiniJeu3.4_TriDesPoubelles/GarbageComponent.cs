using UnityEngine;

public class GarbageComponent : MonoBehaviour
{
    [SerializeField] bool isRecyclable = false;

    public bool IsReciclable => isRecyclable;
}
