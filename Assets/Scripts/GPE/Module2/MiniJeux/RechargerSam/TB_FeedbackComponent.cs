using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TB_FeedbackComponent : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] AudioClip barreBipSon;
    [SerializeField] AudioClip courtCircuitSon;
    [SerializeField] AudioClip erreurSon;
    [SerializeField, VisibleAnywhereProperty] AudioSource audioSource;
    

    public AudioClip BarreBipSon => barreBipSon;
    public AudioClip CourtCircuitSon => courtCircuitSon;
    public AudioClip ErreurSon => erreurSon;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Init()
    {
       
    }

    public void PlaySound(string clipName)
    {
        if(!audioSource || audioClips.Count == 0)
        {
            Debug.Log("AudioSource or AudioClips not set !");
            return;
        }

        int _size = audioClips.Count;
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

    public void PlaySound(AudioClip _clipToPLay)
    {
        if (!audioSource || !_clipToPLay)
        {
            Debug.Log("AudioSource or AudioClips not set !");
            return;
        }

        audioSource.clip = _clipToPLay;
        audioSource.Play();
    }
}
