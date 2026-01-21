using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
struct MailTypePercentage
{
    public MailType  type;
    public float     percentage;
}

public class DepilatorComponent : MonoBehaviour
{
    // Movement info
    [SerializeField] Vector3            direction;              // The direction of the treadmill
    [SerializeField] float              speed;                  // The speed of the treadmill

    // Spawn info
    [SerializeField] float              minSpawnRate;           // Minimum time (seconds) to spawn a mail 
    [SerializeField] float              maxSpawnRate;           // Maximum time (seconds) to spawn a mail 
    [SerializeField]
    List<MailTypePercentage>            percentages;            // List used to initialize the dictionary with the editor
    [SerializeField] Vector3            minSpawnPosition;       // The min position where new mails will be spawned
    [SerializeField] Vector3            maxSpawnPosition;       // The max position where new mails will be spawned
    [SerializeField] Vector3            minSpawnRotation;       // The min rotation where new mails will be spawned
    [SerializeField] Vector3            maxSpawnRotation;       // The max rotation where new mails will be spawned
    [SerializeField] GameObject         toSpawn;                // The gameobject that will be spawned
    Dictionary<MailType, float>         percentagesDictionary;  // Percentage of all types of mails

    // Timer info
    [VisibleAnywhereProperty] float     currentTime;
    [VisibleAnywhereProperty] float     spawnTime;

    List<MailComponent>                 spawnedMails;           // List of all spawned mails

    //[SerializeField] 
    //LetterSortMiniGame                  miniGame = null;


    public int                          LetterCount => spawnedMails.Count;


    void Start()
    {
        InitDictionary();

        spawnedMails = new List<MailComponent>();
        direction = direction.normalized;
        ComputeNewSpawnTime();
    }

    void InitDictionary()
    {
        percentagesDictionary = new Dictionary<MailType, float>();

        foreach (MailTypePercentage _typePercentage in percentages)
            percentagesDictionary[_typePercentage.type] = _typePercentage.percentage;
    }

    void Update()
    {
        float _deltaTime = Time.deltaTime;
        ApplyMovement(_deltaTime);
        UpdateTimer(_deltaTime);
    }

    MailType GetRandomType()
    {
        float _total = 0f;
        foreach (KeyValuePair<MailType, float> _entry in percentagesDictionary)
            _total += _entry.Value;

        float _randomValue = Random.Range(0f, _total);

        float _cumulative = 0f;
        foreach (KeyValuePair<MailType, float> _entry in percentagesDictionary)
        {
            _cumulative += _entry.Value;

            if (_randomValue <= _cumulative)
                return _entry.Key;
        }

        return MailType.Machinable;
    }

    void ApplyMovement(float _deltaTime)
    {
        int _letterCount = LetterCount;
        for (int _index = 0; _index < _letterCount; _index++)
        {
            MailComponent _currentMail = spawnedMails[_index];
            float _speed = speed * _deltaTime;
            _currentMail.transform.position += direction * _speed;
        }
    }

    void ComputeNewSpawnTime()
    {
        spawnTime = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void UpdateTimer(float _deltaTime)
    {
        currentTime += _deltaTime;
        if (currentTime >= spawnTime)
        {
            currentTime = 0.0f;
            ComputeNewSpawnTime();
            SpawnMail();
        }
    }

    void SpawnMail()
    {
        GameObject _go = Instantiate(toSpawn, GetRandomVectorInRange(minSpawnPosition, maxSpawnPosition), Quaternion.Euler(GetRandomVectorInRange(minSpawnRotation, maxSpawnRotation)));
        MailComponent _component = _go.GetComponent<MailComponent>();
        _component.Type = GetRandomType();
        //AddToTreadmill(_component);
    }

    public void AddToTreadmill(MailComponent _component)
    {
        if (!_component) return;
        spawnedMails.Add(_component);
    }

    public void RemoveFromTreadmill(MailComponent _component)
    {
        spawnedMails.Remove(_component);
    }

    public void DestroyMail(MailComponent _component)
    {
        RemoveFromTreadmill(_component);
        Destroy(_component.gameObject);
    }

    Vector3 GetRandomVectorInRange(Vector3 _min, Vector3 _max)
    {
        return new Vector3(
            Random.Range(_min.x, _max.x),
            Random.Range(_min.y, _max.y),
            Random.Range(_min.z, _max.z)
            );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(minSpawnPosition, 0.1f);
        Gizmos.DrawWireSphere(maxSpawnPosition, 0.1f);

        Gizmos.DrawRay((minSpawnPosition + maxSpawnPosition) / 2.0f, direction * speed);

        Gizmos.color = Color.white;
    }
}
