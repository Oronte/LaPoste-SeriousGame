using UnityEngine;

public class MiniJeuBoutons : MiniGame
{
    [VisibleAnywhereProperty] float requiredHoldTime = 0f;
    [SerializeField] ZoneAcceptanceBouton zone = null;
    [SerializeField] float timeLimit = 60.0f;

    float holdTimer = 0f;
    float globalTimer = 0f;

    public override bool IsFinished => holdTimer >= requiredHoldTime;
    public override void StartGame()
    {
        base.StartGame();
        holdTimer = 0f;
        globalTimer = 0f;

        zone.OnExitAccept += ResetHoldTimer;
    }

    void Update()
    {
        if (!IsRunning) return;

        globalTimer += Time.deltaTime;

        if(globalTimer >= timeLimit)
        {
            Debug.Log("Mini-jeu échoué, temps écoulé.");
            StopGame();
            return;
        }

        if(zone.IsInsideAcceptZone)
        {
            holdTimer += Time.deltaTime;

            if(holdTimer >= requiredHoldTime)
            {
                Debug.Log("Mini-jeu réussi");
                StopGame();
            }
        }
    }

    void ResetHoldTimer()
    {
        holdTimer = 0f;
    }

    public override void StopGame()
    {
        base.StopGame();
        zone.OnExitAccept -= ResetHoldTimer;
    }
}
