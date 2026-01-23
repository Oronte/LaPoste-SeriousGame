using UnityEngine;

public interface ISnapper
{
    [SerializeField] public bool IsEnable { get; set; }
    [SerializeField] public GameObject Owner { get; set; }
    void OnSnap();
    void OnUnSnap();
}
