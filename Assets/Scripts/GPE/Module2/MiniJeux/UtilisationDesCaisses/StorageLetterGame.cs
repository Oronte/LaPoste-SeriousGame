using UnityEngine;

public class StorageLetterGame : MiniGame
{
    bool isFinished = false;
    [SerializeField, VisibleAnywhereProperty] public override bool IsFinished => isFinished;
    [SerializeField, VisibleAnywhereProperty] int currentTry = 0;
    [SerializeField] int maxTry = 3;

    public int CurrentTry { get => currentTry; set => currentTry = value; }
    public int MaxTry { get => currentTry; }

    public override void StartGame()
    {
        base.StartGame();
    }

    public override void StopGame()
    {
        base.StopGame();
    }
}
