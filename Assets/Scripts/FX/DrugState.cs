using System;
using UnityEngine;

[Serializable]
public class DrugState : CameraState
{
    [SerializeField] private Transform realHeadTarget;

    [SerializeField] private DruggedEffect cameraEffectScript;

    public override void Trigger()
    {
        if (cameraEffectScript == null)
        {
            return;
        }

        cameraEffectScript.targetEye = realHeadTarget;

        cameraEffectScript.enabled = true;

        Debug.Log("Mode Drogué activé.");
    }

    public void Exit()
    {
        if (cameraEffectScript != null) cameraEffectScript.enabled = false;
    }
}
