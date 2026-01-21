using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevelComponent : MonoBehaviour
{
    [Tooltip("Nom de la Scene à Load"), SerializeField] string sceneToLoad = "";

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
