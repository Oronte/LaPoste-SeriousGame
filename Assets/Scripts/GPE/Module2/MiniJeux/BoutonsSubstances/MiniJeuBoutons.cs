using UnityEngine;

public class MiniJeuBoutons : MiniGame
{
    [Header("Timers")]
    [SerializeField] float requiredHoldTime = 5f;
    [SerializeField] float timeLimit = 60.0f;

    [Header("Zone Reference")]
    [SerializeField] ZoneAcceptanceBouton zone = null;

    float holdTimer = 0f;
    float globalTimer = 0f;

    public override bool IsFinished => holdTimer >= requiredHoldTime;
    public override void StartGame()
    {
        base.StartGame();
        holdTimer = 0f;
        globalTimer = 0f;

        Debug.Log("[MiniJeuBoutons] Jeu démarré");

        zone.OnEnterAccept += HandleEnterAccept;
        zone.OnExitAccept += HandleExitAccept;
        zone.OnEnterFail += HandleEnterFail;
    }

    void Update()
    {
        if (!IsRunning) return;

        globalTimer += Time.deltaTime;

        if(globalTimer >= timeLimit)
        {
            Debug.Log("[MiniJeuBoutons] Echec : temps écoulé.");
            StopGame();
            return;
        }

        if(zone.IsInsideAcceptZone)
        {
            holdTimer += Time.deltaTime;
            Debug.Log($"[MiniJeuBoutons] HoldTimer = {holdTimer:F2}");

            if(holdTimer >= requiredHoldTime)
            {
                Debug.Log("[MiniJeuBoutons] Succès : bouton maintenu 5 secondes.");
                StopGame();
            }
        }
    }

    void HandleEnterAccept()
    {
        Debug.Log("[MiniJeuBoutons] Entrée dans la zone ACCEPT.");
    }

    void HandleExitAccept()
    {
        Debug.Log("[MiniJeuBoutons] Sortie dans la zone ACCEPT -> reset du timer.");
        holdTimer = 0f;
    }

    void HandleEnterFail()
    {
        Debug.Log("[MiniJeuBoutons] Entrée dans la zone FAIL -> reset du timer.");
        holdTimer = 0f;
    }

    public override void StopGame()
    {
        base.StopGame();
        Debug.Log("[MiniJeuBoutons] Jeu arrêté.");

        zone.OnEnterAccept -= HandleEnterAccept;
        zone.OnExitAccept -= HandleExitAccept;
        zone.OnEnterFail -= HandleEnterFail;
    }
}
