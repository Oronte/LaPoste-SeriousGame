using UnityEngine;

public class StateCameraComponent : MonoBehaviour
{
    CameraState currentState = null; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ApplyState<T>() where T : CameraState
    {

    }
        
}
