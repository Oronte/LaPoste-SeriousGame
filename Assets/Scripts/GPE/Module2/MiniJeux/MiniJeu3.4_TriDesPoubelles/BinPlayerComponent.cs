using UnityEngine;

public class BinPlayerComponent : MonoBehaviour
{
    AudioSource audio = null;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play(); // TODO mettre musique
    }

    public void StopAudio()
    {
        audio.Stop();
    }
}
