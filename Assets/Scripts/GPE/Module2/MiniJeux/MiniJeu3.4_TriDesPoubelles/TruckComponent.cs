using UnityEngine;

public class TruckComponent : MonoBehaviour
{
    //[SerializeField] Vector3 aPos;
    //[SerializeField] Vector3 bPos;
    //[SerializeField] float time;
    AudioSource audio = null;
    MeshRenderer mesh = null;
    bool isTrigger = false;
    bool isColliding = false;
    BinPlayerComponent player = null;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float securityDist = 3.0f;
    //float currentTime;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
        if (!mesh) return;
        mesh.enabled = false;
    }

    private void OnTriggerEnter(Collider _other)
    {
        BinPlayerComponent _player = _other.GetComponent<BinPlayerComponent>();
        if (!_player) return;
        isTrigger = true;
        player = _player;
        mesh.enabled = true;
    }

    void MoveToPlayer()
    {
        if (!isTrigger || isColliding) return;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void CheckPlayerDist()
    {
        if (isColliding || !player) return;

        float _dist = Vector3.Distance(transform.position, player.transform.position);

        if (_dist <= securityDist)
        {
            StopTruck();
            player.StopAudio();
        }
            
    }

    void Update()
    {
        CheckPlayerDist();
        MoveToPlayer();

        //if (isTrigger) return;
        //ComputeMovement();
        //UpdateTime();
    }

    //void ComputeMovement()
    //{
    //    transform.position = Vector3.Lerp(aPos, bPos, currentTime / time);
    //}

    //void UpdateTime()
    //{
    //    currentTime += Time.deltaTime;
    //    if (currentTime >= time)
    //    {
    //        currentTime = 0.0f;
    //        SwapPos();
    //    }
    //}

    //void SwapPos()
    //{
    //    Vector3 _temp = aPos;
    //    aPos = bPos;
    //    bPos = _temp;
    //}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        //Gizmos.DrawWireSphere(aPos, 0.5f);
        //Gizmos.DrawLine(bPos, aPos);
        //Gizmos.DrawWireSphere(bPos, 0.5f);

        Gizmos.DrawWireSphere(transform.position, securityDist);

        Gizmos.color = Color.white;
    }

    public void StopTruck()
    {
        if (!isColliding) audio.Play(); // TODO mettre bruit de camion
        isColliding = true;
    }
}
