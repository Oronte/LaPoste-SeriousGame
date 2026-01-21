using UnityEngine;

public class StateCameraComponent : MonoBehaviour
{
    [SerializeField] CameraState currentState = null;
    [SerializeField] Camera ownerCamera = null;

    void Start()
    {
        ApplyState<DrugState>();
    }

    void Update()
    {
        
    }

    public void ApplyState<T>() where T : CameraState, new()
    {
        currentState = new T();
        currentState.Init(ownerCamera.transform, transform);
        currentState.Enter();
    }

    private void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.Trigger();
        }
    }

}
