using UnityEngine;

public class TruckSpawner : MonoBehaviour
{
    [SerializeField] GameObject gameObject = null;

    private void OnTriggerEnter(Collider _other)
    {
        BinPlayerComponent _player = _other.GetComponent<BinPlayerComponent>();
        if (!_player || !gameObject) return;
        gameObject.SetActive(true);
    }
}
