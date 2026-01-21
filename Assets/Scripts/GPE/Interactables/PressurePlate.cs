using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent<bool> onPressurePlateToggle = null;

    private void OnCollisionEnter(Collision collision) => onPressurePlateToggle?.Invoke(true);
    private void OnCollisionExit(Collision collision) => onPressurePlateToggle?.Invoke(false);


}
