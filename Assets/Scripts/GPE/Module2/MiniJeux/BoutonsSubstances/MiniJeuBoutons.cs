using UnityEngine;

public class MiniJeuBoutons : MiniGame
{
    public override bool IsFinished => throw new System.NotImplementedException();
    public override void StartGame()
    {
        base.StartGame();
        GameManager.Instance.GetMiniGame<MiniJeuBoutons>();
    }
}
