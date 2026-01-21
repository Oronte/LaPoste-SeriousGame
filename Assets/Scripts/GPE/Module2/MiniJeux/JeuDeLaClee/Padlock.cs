using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Padlock : MonoBehaviour
{
    //Evement apeller quand le cadena est ouvert
    public event Action OnUnlock = null;
    //Reference vers le mini jeu
    KeyGame keyGame = null;
    //Active le debugger de l'objet (Log)
    [SerializeField, Tooltip("Active le debugger de l'objet (Log)")] bool useDebug = false;
    //
    [SerializeField, Tooltip("Son du cadena à la key alignée")] AudioClip clickSound = null;
    [SerializeField, Tooltip("Son du cadena dévérouiller")] AudioClip unlockSound = null;
    AudioSource audioSource = null;
    ParticleSystem particlesSys = null;
    private void Start()
    {
        //Retarde l'apelle pour que la liste de mini jeu soit bien initialiser
        Invoke(nameof(LateInit), 0.5f);
    }

    /// <summary>
    /// Initialise les variables
    /// </summary>
    void LateInit()
    {
        if (!GameManager.Instance) return;
        audioSource = GetComponent<AudioSource>();
        if (!audioSource) audioSource = GetComponentInChildren<AudioSource>();
        particlesSys = GetComponentInChildren<ParticleSystem>();
        keyGame = GameManager.Instance.GetMiniGame<KeyGame>();
        OnUnlock += FinishKeyGame;
        OnUnlock += PlayClickSound;
        OnUnlock += PlayParticlesUnlock;
    }

    void PlayParticlesUnlock()
    {
        if (!particlesSys) return;
        particlesSys.Play();
    }

    void PlayClickSound()
    {
        if(!audioSource || !clickSound) return;
        audioSource.clip = clickSound;
        audioSource.Play();
        //if (!unlockSound) return;
        //audioSource.clip = unlockSound;
        //audioSource.PlayDelayed(2.0f);
    }

    /// <summary>
    /// Ouvre la cadena de l'extérieur
    /// </summary>
    /// <param name="_key">Clée qui ouvre le cadena</param>
    public void UnlockPadlock(Key _key)
    {
        if (!_key) return;
        OnUnlock?.Invoke();
    }

    /// <summary>
    /// Fini le mini jeu (abboner a l'événement d'ouvertur du cadena)
    /// </summary>
    void FinishKeyGame()
    {
        if(!keyGame) return;
        keyGame.UnlockPadlock();
        if (useDebug) Debug.Log("Finish Key Game !");
    }
}
