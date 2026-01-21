using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public interface IEnabler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Enable();
    public void Disable();
   
}
