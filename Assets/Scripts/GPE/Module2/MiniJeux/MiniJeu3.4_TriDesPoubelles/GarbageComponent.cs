using UnityEngine;

public class GarbageComponent : MonoBehaviour
{
    [SerializeField] bool isRecyclable = false;

    public bool IsReciclable => isRecyclable;

    void Start()
    {
        GetComponent<MeshRenderer>().material.color = isRecyclable ? Color.yellow : Color.magenta;
    }
}
