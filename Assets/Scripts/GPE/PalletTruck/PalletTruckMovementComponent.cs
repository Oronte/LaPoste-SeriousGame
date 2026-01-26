using System.Net.Sockets;
using System.Runtime.InteropServices;
using UnityEngine;

public class PalletTruckMovementComponent : MonoBehaviour
{
    public WheelCollider rearWheel;
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public Transform throttleTransform;
    public Transform steeringWheel;

    public float maxMotorTorque = 15000.0f;
    public float maxAngle = 30.0f;
    public float tolerance = 10.0f;

    /// <summary>
    /// Is vehicle going forward or backwards
    /// </summary>
    public bool Forward { get; set; } = true;

    private void Start()
    {
        rearWheel.steerAngle = 90.0f;
        frontLeftWheel.steerAngle = 90.0f;
        frontRightWheel.steerAngle = 90.0f;
    }

    void FixedUpdate()
    {
        ManageNouvement();

        ManageDirection();
    }

    private void ManageDirection()
    {
        float _currentAngle = Vector3.SignedAngle(transform.forward, steeringWheel.forward, transform.up);

        if (Mathf.Abs(_currentAngle) < tolerance)
        {
            rearWheel.steerAngle = 90.0f;
            return;
        }
        rearWheel.steerAngle = (_currentAngle / 3.0f) + 90.0f;
    }

    private void ManageNouvement()
    {
        float _currentAngle = throttleTransform.localEulerAngles.y;
        if (_currentAngle > 180.0f) _currentAngle -= 360.0f;

        if (Mathf.Abs(_currentAngle) < tolerance)
        {
            rearWheel.motorTorque = 0.0f;
            frontLeftWheel.brakeTorque = maxMotorTorque;
            frontRightWheel.brakeTorque = maxMotorTorque;
            rearWheel.brakeTorque = maxMotorTorque;
            return;
        }

        float _inputFactor = Mathf.Clamp(_currentAngle / maxAngle, -1.0f, 1.0f);

        rearWheel.motorTorque = _inputFactor * maxMotorTorque;

        frontLeftWheel.brakeTorque = 0.0f;
        frontRightWheel.brakeTorque = 0.0f;
        rearWheel.brakeTorque = 0.0f;
    }

    public void Brake()
    {
        Debug.Log("Braking");
        // TODO
    }
}
