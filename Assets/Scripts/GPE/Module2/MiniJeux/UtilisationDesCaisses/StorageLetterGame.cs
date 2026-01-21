using UnityEngine;
using UnityEngine.UI;

public class StorageLetterGame : MiniGame
{
    bool isFinished = false;
    [SerializeField, VisibleAnywhereProperty] public override bool IsFinished => isFinished;
    [SerializeField, VisibleAnywhereProperty] int currentTry = 0;
    [SerializeField] int maxTry = 3;

    public int CurrentTry { get => currentTry; set => currentTry = value; }
    public int MaxTry { get => currentTry; }

    [Header("Components", order = 1)]
    [SerializeField] WedgeComponent wedge = null;
    [SerializeField] LetterPileComponent letterPile = null;
    [SerializeField] RadialProgressBar progressBar = null;
    [SerializeField] Button nextLevelButton = null;
    [SerializeField] Button retryButton = null;

    public override void StartGame()
    {
        base.StartGame();
        wedge.Init();
        letterPile.Init();
        progressBar.Init();
        progressBar.OnMinValue.AddListener(ShowRestartMenu);
    }

    public override void StopGame()
    {
        base.StopGame();
    }

    void ShowRestartMenu()
    {
        Debug.Log("Player lost, waiting to retry or continue to next level");
        nextLevelButton.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        //If next :
        nextLevelButton.onClick.AddListener(StopGame);
        //If retry :
        retryButton.onClick.AddListener(RestartGame);

    }

    void RestartGame()
    {
        letterPile.Reset();
        wedge.Reset();
        progressBar.Reset();
        nextLevelButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

    }
}
