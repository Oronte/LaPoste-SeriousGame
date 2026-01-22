using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ZoneAcceptanceBouton : MonoBehaviour
{
    public event Action OnEnterAccept = null;
    public event Action OnExitAccept = null;

    public event Action OnEnterFail = null;
    public event Action OnExitFail = null;

    [Header("References")]
    [SerializeField] Collider acceptZone = null;
    [SerializeField] Collider failZone = null;

    [Header("Interactor")]
    [SerializeField] List<XRPokeInteractor> pokeInteractors = new List<XRPokeInteractor>();

    public bool IsInsideAcceptZone { get; private set; } = false;
    public bool IsInsideFailZone { get; private set; } = false;

    void Update()
    {
        bool _inAccept = false;
        bool _inFail = false;

        foreach(XRPokeInteractor _interactor in pokeInteractors)
        {
            if (!_interactor) continue;

            Vector3 _pokePos = _interactor.attachTransform.position;

            if(acceptZone.bounds.Contains(_pokePos))
            {
                _inAccept = true;
            }
            if(failZone.bounds.Contains(_pokePos))
            {
                _inFail = true;
            }

            Debug.DrawRay(_pokePos, Vector3.up * 0.05f, Color.blue);
        }

        if (_inAccept && !IsInsideAcceptZone)
        {
            OnEnterAccept?.Invoke();
        }
        if (!_inAccept && IsInsideAcceptZone)
        {
            OnExitAccept?.Invoke();
        }

        if (_inFail && !IsInsideFailZone)
        {
            OnEnterFail?.Invoke();
        }
        if (!_inFail && IsInsideFailZone)
        {
            OnExitFail?.Invoke();
        }

        IsInsideAcceptZone = _inAccept;
        IsInsideFailZone = _inFail;
    }

    private void OnDrawGizmos()
    {
        if (!acceptZone) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(acceptZone.bounds.center, acceptZone.bounds.size);

        if (!failZone) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(failZone.bounds.center, failZone.bounds.size);
    }
}