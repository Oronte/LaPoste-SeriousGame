using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class KeepPressedPokeInteractable : XRBaseInteractable
{
    public bool IsPressed { get; private set; } = false;
    public UnityEvent whilePressed = new UnityEvent();

    protected override void OnSelectEntered(SelectEnterEventArgs _args)
        => IsPressed = true;

    protected override void OnSelectExited(SelectExitEventArgs _args)
        => IsPressed = false;

    private void Update()
    {
        if (IsPressed)
            whilePressed?.Invoke();
    }
}

