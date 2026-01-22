using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent (typeof(AudioSource))]
public class Key : MonoBehaviour
{

    [SerializeField, Tooltip("Active le debugger de l'objet (Log)")] bool useDebug = false;
    [SerializeField, Tooltip("Son contre le metal du cadena")] AudioClip missPadlockSound = null;
    [SerializeField, VisibleAnywhereProperty, Tooltip("Transform contenant le particle Component")] Transform particlePos = null;
    AudioSource audioSource = null;
    ParticleSystem particlesSys = null;
    private void Start()
    {
        //Invoke(nameof(Init), 0.5f);
        Init();
    }


    void Init()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource) audioSource = GetComponentInChildren<AudioSource>();
        particlesSys = GetComponentInChildren<ParticleSystem>();
        particlePos = particlesSys?.transform;
        if (!GameManager.Instance) return;
        KeyGame _keyGame = GameManager.Instance.GetMiniGame<KeyGame>();
        if(!_keyGame) return;
        _keyGame.CurrentKey = this;
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
        if (!_padlock || !particlesSys ||!audioSource) return;
        audioSource.clip = missPadlockSound;
        audioSource.Play();
        particlesSys.Play();
        if (!particlePos || _collision.contactCount < 1) return;
        particlePos.position = _collision.contacts[0].point;
    }

    /// <summary>
    /// Récupére le composant de gravité et desactive la gravité
    /// </summary>
    /// <param name="_isEnable">si la gravité doit etre activé ou non</param>
    public void UpdateGravity(bool _isEnable)
    {
        if (!TryGetComponent<Rigidbody>(out Rigidbody _rb)) return;
        _rb.useGravity = _isEnable;
        _rb.isKinematic = !_isEnable;
    }
    /// <summary>
    /// Récupére le composant de d'interaction et desactive l'interaction
    /// </summary>
    /// <param name="_isEnable">si l'interaction doit etre activé ou non</param>
    public void UpdateGrabbable(bool _isEnable)
    {
        if (!TryGetComponent<XRGrabInteractable>(out XRGrabInteractable _grab)) return;
        _grab.enabled = _isEnable;
    }
}
