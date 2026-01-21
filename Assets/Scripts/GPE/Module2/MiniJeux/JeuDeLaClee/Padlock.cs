using System;
using UnityEngine;

public class Padlock : MonoBehaviour
{
    //Evement apeller quand le cadena est ouvert
    public event Action OnUnlock = null;
    KeyGame keyGame = null;
    //[SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Le composant de collision qui detect si la clée est insérer (à ne pas toucher)")] Collider unlockCollider = null;
    [SerializeField, Tooltip("Active le debugger de l'objet (Log)")] bool useDebug = false;
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
        keyGame = GameManager.Instance.GetMiniGame<KeyGame>();
        OnUnlock += FinishKeyGame;
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
