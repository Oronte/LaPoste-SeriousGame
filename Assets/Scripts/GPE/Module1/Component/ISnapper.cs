using UnityEngine;

public interface ISnapper
{
    [SerializeField] public bool IsEnable { get; set; }
    void OnSnap();
    void OnUnSnap();
}
