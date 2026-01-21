using Unity.XR.CoreUtils;
using UnityEngine;

public class PalletTruck : MonoBehaviour
{
    [SerializeField] LerpComponent lift;
    [SerializeField] float intensity;
    [SerializeField] public bool Active { get; set; } = false;

    void Start()
    {
    }

    void Update()
    {
    }
}
