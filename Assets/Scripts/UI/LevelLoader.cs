using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public string levelName;
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
