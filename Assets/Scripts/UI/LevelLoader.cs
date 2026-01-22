using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("References")]
    [SerializeField, DisplayText("Fader (Optionnal)")] Fader fader = null;

    [Header("Config")]
    [SerializeField] public string levelName;

    bool isTransitioning = false;

    private void Start()
    {
        if (fader)
        {
            fader.OnFadeFinished.AddListener(OnFadeComplete);
        }
    }

    public void LoadLevel(string _levelName)
    {
        if (isTransitioning) return;

        levelName = _levelName;
        isTransitioning = true;

        if (fader)
            fader.FadeIn();
        else
            ChangeLevel();
    }

    public void OnFadeComplete()
    {
        if (isTransitioning)
        {
            ChangeLevel();
        }
    }

    public void ChangeLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void RetryLevel()
    {
        levelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(levelName);
    }
}
