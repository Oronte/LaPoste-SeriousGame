using UnityEngine;

public class LerpComponent : MonoBehaviour
{
    [SerializeField] Transform firstSocket = null;
    [SerializeField] Transform secondSocket = null;
    [SerializeField] bool constantSpeed = false;
    [SerializeField, HideCondition("constantSpeed", _invert:true)] float lerpTime = 0f; 
    [SerializeField, HideCondition("constantSpeed")] float lerpSpeed = 0f;

    bool isLerping = false;
    bool isFirst = true;
    float currentTime = 0f;

    Vector3 targetPos = Vector3.zero;
    Vector3 basePos = Vector3.zero;

    private void Start()
    {
        targetPos = GetSocket(!isFirst).position;
        basePos = GetSocket(isFirst).position;
    }

    public void StartLerp()
    {
        if (isLerping) return;

        isLerping = true;

    }


    public void LerpToFirst() => InternalLerpWrapper(true);

    public void LerpToSecond() => InternalLerpWrapper(false);

    private void InternalLerpWrapper(bool _toFirst)
    {
        targetPos = GetSocket(_toFirst).position;
        basePos = GetSocket(!_toFirst).position;

        StartLerp();
    }


    private void Update()
    {
        if (!isLerping) return;

        if (!constantSpeed)
        {
            currentTime += Time.deltaTime;
            LerpByTime();
        }
        else
            LerpBySpeed();
    }

    void LerpByTime()
    {
        Vector3 _pos = Vector3.Lerp(basePos, targetPos, currentTime/lerpTime);
        transform.position = _pos;

        CheckDestination();
    }

    void LerpBySpeed()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        CheckDestination();
    }

    void CheckDestination()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            isLerping = false;
            isFirst = !isFirst;
            currentTime = 0f;

            Vector3 _temp = basePos;
            basePos = targetPos;
            targetPos = _temp;
        }
    }

    private Transform GetSocket(bool _first) => _first ? firstSocket : secondSocket;

}
