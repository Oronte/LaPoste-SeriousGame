using UnityEngine;
using UnityEngine.Audio;

public class TB_FeedbackComponent : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField, VisibleAnywhereProperty] AudioSource audioSource;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clipName)
    {
        if(!audioSource || audioClips.Length == 0)
        {
            Debug.Log("AudioSource or AudioClips not set !");
            return;
        }

        int _size = audioClips.Length;
        for (int _i = 0; _i < _size; _i++)
        {
            if (audioClips[_i].name == clipName)
            {
                audioSource.clip = audioClips[_i];
                audioSource.Play();
                return;
            }
        }
        Debug.Log("Clip: " + clipName + " not found !");
    }
}
