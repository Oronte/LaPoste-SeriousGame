using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent<bool> onPressurePlateToggle = null;

    private void OnTriggerEnter(Collider _col) => onPressurePlateToggle?.Invoke(true);
    private void OnTriggerExit(Collider _col) => onPressurePlateToggle?.Invoke(false);


}
