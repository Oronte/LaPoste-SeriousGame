using UnityEngine;
using UnityEngine.Audio;

public class AudioMixeurEdit : MonoBehaviour
{
    [SerializeField] AudioMixer mixeur = null;
    [SerializeField] string volumeParameter = "";

    private void Start()
    {
        if(PlayerPrefs.HasKey(volumeParameter))
        {
            LoadVolume();
        }
    }

    public void SetVolume(float _volume)
    {
        if (!mixeur)
            return;

        mixeur.SetFloat(volumeParameter, Mathf.Log10(_volume)*20);
        // Save prefs
        PlayerPrefs.SetFloat(volumeParameter, _volume);
    }

    void LoadVolume()
    {
        // Load prefs
        SetVolume(PlayerPrefs.GetFloat(volumeParameter));
    }
}
