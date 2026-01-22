using NUnit.Framework.Internal;
using UnityEngine;

public class KeyGame : MiniGame
{
    public override bool IsFinished => unlockPadlock;
    //Es ce que le cadena est encore verouiller
    [SerializeField, VisibleAnywhereProperty, Tooltip("Es ce que le cadena est encore verouiller")]bool unlockPadlock = false;
    Key currentKey = null;
    Padlock currentPadlock = null;

    public Key CurrentKey 
    { 
        get => currentKey;
        set
        {
            if (currentKey || !value) return;
            currentKey = value;
        }
    }
    public Padlock CurrentPadlock
    {
        get => currentPadlock;
        set
        {
            if (currentPadlock || !value) return;
            currentPadlock = value;
        }
    }
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
