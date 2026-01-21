using UnityEngine;

public class DruggedEffect : MonoBehaviour
{
    [HideInInspector] public Transform targetEye; 

    public float positionSmoothTime = 0.3f;
    public float rotationSmoothTime = 0.3f;

    private Vector3 currentVelocity;

    void OnEnable()
    {
        if (targetEye != null)
        {
            transform.position = targetEye.position;
            transform.rotation = targetEye.rotation;
        }
    }

    void LateUpdate()
    {
        if (targetEye == null) return;

        transform.position = Vector3.SmoothDamp(transform.position, targetEye.position, ref currentVelocity, positionSmoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetEye.rotation, Time.deltaTime / rotationSmoothTime);
    }
}