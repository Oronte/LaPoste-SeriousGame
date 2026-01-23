using UnityEngine;

public class BinPlayerComponent : MonoBehaviour
{
    AudioSource audio = null;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play(); // TODO mettre musique
    }

    private void OnTriggerEnter(Collider _other)
    {
        TruckComponent _truck = _other.GetComponent<TruckComponent>();
        if (!_truck) return;
        audio.Stop();
        _truck.StopTruck();
    }
}
