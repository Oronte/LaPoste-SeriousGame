using System;
using UnityEngine;

[Serializable]
public class DrugState : CameraState
{
    public DruggedEffect cameraEffectScript;
    public float positionSmoothTime = 0.3f;
    public float rotationSmoothTime = 0.3f;

    private Vector3 currentVelocity;


    public override void Enter()
    {
    }

    public override void Trigger()
    {

        //transform.position = Vector3.SmoothDamp(transform.position, realHeadTarget.position, ref currentVelocity, positionSmoothTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, realHeadTarget.rotation, Time.deltaTime / rotationSmoothTime);

        Debug.Log("Mode Drogué activé.");
    }

    public void Exit()
    {
        if (cameraEffectScript != null) cameraEffectScript.enabled = false;
    }


}
