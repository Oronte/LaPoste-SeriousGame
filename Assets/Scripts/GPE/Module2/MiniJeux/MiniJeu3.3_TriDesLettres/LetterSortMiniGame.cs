using UnityEngine;

public class LetterSortMiniGame : MiniGame
{
    [VisibleAnywhereProperty] float     currentTime = 0f;
    [SerializeField] float              timeLimit = 45.0f;
    public bool hasDroppedMail = false;
    public int discardedNonMachinableMailCount = 0;
    public bool ke7ContainsMachnable = false;

    public override bool                IsFinished => ComputeIsFinished();

    void Update()
    {
        UpdateTimer(Time.deltaTime);
        //base.Update();
    }

    void UpdateTimer(float _deltaTime)
    {
        currentTime += _deltaTime;
    }

    bool ComputeIsFinished()
    {
        return currentTime >= timeLimit ||
               hasDroppedMail           ||
               ke7ContainsMachnable     ||
               discardedNonMachinableMailCount > 0;
    }
}
