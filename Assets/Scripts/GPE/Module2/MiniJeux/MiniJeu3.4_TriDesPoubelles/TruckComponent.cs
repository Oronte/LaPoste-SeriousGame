using UnityEngine;

public class TruckComponent : MonoBehaviour
{
    [SerializeField] Vector3 aPos;
    [SerializeField] Vector3 bPos;
    [SerializeField] float time;
    AudioSource audio = null;
    bool isTrigger = false;
    float currentTime;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isTrigger) return;
        ComputeMovement();
        UpdateTime();
    }

    void ComputeMovement()
    {
        transform.position = Vector3.Lerp(aPos, bPos, currentTime / time);
    }

    void UpdateTime()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            currentTime = 0.0f;
            SwapPos();
        }
    }

    void SwapPos()
    {
        Vector3 _temp = aPos;
        aPos = bPos;
        bPos = _temp;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(aPos, 0.5f);
        Gizmos.DrawLine(bPos, aPos);
        Gizmos.DrawWireSphere(bPos, 0.5f);

        Gizmos.color = Color.white;
    }

    public void StopTruck()
    {
        if (!isTrigger) audio.Play(); // TODO mettre bruit de camion
        isTrigger = true;
    }
}
