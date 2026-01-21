using NUnit.Framework.Internal;
using UnityEngine;

public class KeyGame : MiniGame
{
    public override bool IsFinished => unlockPadlock;
    //Es ce que le cadena est encore verouiller
    [SerializeField, VisibleAnywhereProperty, Tooltip("Es ce que le cadena est encore verouiller")]bool unlockPadlock = false;

    public override void StartGame()
    {
        base.StartGame();
        unlockPadlock = false;
    }
    public override void StopGame()
    {
        base.StopGame();
        Score = MaxScore;
        unlockPadlock = false;
    }

    public void UnlockPadlock()
    {
        unlockPadlock = true;
    }
}
