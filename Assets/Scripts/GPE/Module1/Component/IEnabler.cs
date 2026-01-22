using UnityEngine;

public interface IEnabler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public bool IsEnable { get; set; }

    public void Enable();
    public void Disable();

}
