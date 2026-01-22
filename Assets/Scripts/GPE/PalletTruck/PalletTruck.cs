using Unity.XR.CoreUtils;
using UnityEngine;

public class PalletTruck : MonoBehaviour
{
    [SerializeField] LerpComponent lift;
    [SerializeField] float intensity;
    [SerializeField, VisibleAnywhereProperty] bool active = false;
    public bool Active
    {
        get => active;
        set => active = value;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void EmergencyStop()
    {
        Active = false;
        Debug.Log("Emergency stop");
    }
}
