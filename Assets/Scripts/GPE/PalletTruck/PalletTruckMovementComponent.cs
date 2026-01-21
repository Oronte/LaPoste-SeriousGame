using System.Net.Sockets;
using UnityEngine;

public class PalletTruckMovementComponent : MonoBehaviour
{
    public WheelCollider rearWheel;
    public Transform throttleTransform;

    public float maxMotorTorque = 15000.0f;
    public float maxAngle = 45.0f;

    void FixedUpdate()
    {
        float _currentAngle = throttleTransform.localEulerAngles.z;

        if (_currentAngle > 180.0f) _currentAngle -= 360.0f;

        Debug.Log(_currentAngle);
        float _inputFactor = Mathf.Clamp(_currentAngle, -1.0f, 1.0f);

        rearWheel.motorTorque = _inputFactor * maxMotorTorque;
    }
}
