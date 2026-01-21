using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Key : MonoBehaviour
{

    [SerializeField, Tooltip("Active le debugger de l'objet (Log)")] bool useDebug = false;
    [SerializeField, Tooltip("Son contre le metal du cadena")] AudioClip missPadlockSound = null;
    AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(!audioSource) audioSource = GetComponentInChildren<AudioSource>();
    }
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
    /// <summary>
    /// Lors de la collision contre un autre object on verifie si c'est un cadena et on joue le son du metal
    /// </summary>
    /// <param name="_collision"></param>
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision == null || !_collision.collider || !missPadlockSound || !audioSource) return;
        Padlock _padlock = _collision.collider.GetComponent<Padlock>();
        if (!_padlock) _padlock = _collision.collider.GetComponentInChildren<Padlock>();
        if (!_padlock) _padlock = _collision.collider.GetComponentInParent<Padlock>();
        if (!_padlock) return;
        audioSource.clip = missPadlockSound;
        audioSource.Play();
    }
}
