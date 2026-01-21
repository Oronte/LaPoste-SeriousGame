using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField, Tooltip("Active le debugger de l'objet (Log)")] bool useDebug = false;
    /// <summary>
    /// Lors de la collision dans une zone on verifie si la zone est une zone de deblocage de cadena
    /// </summary>
    /// <param name="_other">collision rencontrer</param>
    private void OnTriggerEnter(Collider _other)
    {
        if (!_other) return;
        Padlock _padlock = _other.GetComponent<Padlock>();
        if (!_padlock) _padlock = _other.GetComponentInChildren<Padlock>();
        if(!_padlock) _padlock = _other.GetComponentInParent<Padlock>();
        if(!_padlock) return;
        _padlock.UnlockPadlock(this);
        if (useDebug) Debug.Log("Unlock padlock (with key)!");
    }
}
