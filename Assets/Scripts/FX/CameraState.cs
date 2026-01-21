using System;
using UnityEngine;

[Serializable]
public abstract class CameraState
{
    protected Transform realHeadTarget;
    protected Transform transform;

    public void Init(Transform _realHeadTarget, Transform _transform)
    {
        realHeadTarget = _realHeadTarget;
        transform = _transform;
    }

    public abstract void Enter();

    public abstract void Trigger();
}
